using Google.Protobuf;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PM_Common.DTO;
using PM_Common.DTO.Parking.Event;
using PM_Common.DTO.Report;
using PM_Common.Enums;
using PM_Common.Enums.Report;
using PM_DAL;
using PM_DAL.Entity;
using PM_DAL.UOW;
using System.Net.WebSockets;
using System.Text;

namespace PM_API.Services
{
    public class ExportService : Export.ExportBase
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<ExportService> _logger;

        public ExportService(IDbContext<IUnitOfWork> uow, ILogger<ExportService> logger)
        {
            _uow    = uow;
            _logger = logger;
        }

        public async override Task ExportReportByDate(ExportReportByDateRequest request, IServerStreamWriter<ExportReportByDateResponse> responseStream, ServerCallContext context)
        {
            using var uow = await _uow.OpenAsync();

            _logger.LogInformation("Export By Date has been started.");

            try
            {
                ExportReportByDateResponse chunkedData = new ();

                var filter = new ReportByDateFilterDto()
                {
                    ParkingLotId = request.ParkingLotId,
                    ReportType   = (ReportType)request.ReportType,
                    ForDate      = DateTime.Parse(request.ReportForDate),
                };

                int chunkSize = 10;

                foreach (var chunk in uow.ParkingEventLogRepository.GetParkingEventReportByDateStreamed(filter, chunkSize, context.CancellationToken))
                {
                    chunkedData.ParkingEventReport.Add(new ExportParkingEventReport(){
                        EventId    = chunk.EventId,
                        EventLogId = chunk.EventLogId,
                        EventDate  = chunk.EventDate.ToString(),
                        EventName  = chunk.EventName,
                    });
                 
                    if (chunkedData.ParkingEventReport.Count >= chunkSize)
                    {
                        await responseStream.WriteAsync(chunkedData);

                        chunkedData.ParkingEventReport.Clear();
                    }

                    //await Task.Delay(200);
                }

                if(chunkedData.ParkingEventReport.Count > 0)
                {
                    await responseStream.WriteAsync(chunkedData);
                }
            } 
            catch (RpcException rpcEx)
            {
                _logger.LogInformation($"RPC Exception. Export Failed. ${rpcEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Export Failed. ${ex.Message}");
            }


            /* using (NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = postgres; UserId = pmdb_admin; Password = pmdb1234; CommandTimeout = 60"))
             {
                 conn.Open();

                 NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM parking_event_log", conn);

                 NpgsqlDataReader dataReader = cmd.ExecuteReader();

                 while (dataReader.HasRows)
                 {
                     var anyRows = await dataReader.ReadAsync();

                     if (anyRows)
                     {
                         Console.WriteLine(dataReader.GetValue(0));

                         await responseStream.WriteAsync(new ExportReportResponse()
                         {
                             EventId = (long)dataReader.GetValue(0)
                         });
                     }
                     else
                     {
                         break;
                     }
                 }

                 dataReader.Close();
             }
            */
            return;
        }

        public async override Task ExportReportByPeriod(ExportReportByPeriodRequest request, IServerStreamWriter<ExportReportByPeriodResponse> responseStream, ServerCallContext context)
        {
            using var uow = await _uow.OpenAsync();

            _logger.LogInformation("Export By Period has been started.");

            try
            {
                ExportReportByPeriodResponse chunkedData = new();

                var filter = new ReportByPeriodFilterDto()
                {
                    ParkingLotId = request.ParkingLotId,
                    ReportType   = (ReportType)request.ReportType,
                    FromDate     = DateTime.Parse(request.ReportFromDate),
                    ToDate       = DateTime.Parse(request.ReportToDate),
                };

                int chunkSize = 10;

                foreach (var chunk in uow.ParkingEventLogRepository.GetParkingEventReportByPeriodStreamed(filter, chunkSize, context.CancellationToken))
                {
                    chunkedData.ParkingEventReport.Add(new ExportParkingEventReport()
                    {
                        EventId    = chunk.EventId,
                        EventLogId = chunk.EventLogId,
                        EventDate  = chunk.EventDate.ToString(),
                        EventName  = chunk.EventName,
                    });

                    if (chunkedData.ParkingEventReport.Count >= chunkSize)
                    {
                        await responseStream.WriteAsync(chunkedData);

                        chunkedData.ParkingEventReport.Clear();
                    }

                }

                if (chunkedData.ParkingEventReport.Count > 0)
                {
                    await responseStream.WriteAsync(chunkedData);
                }
            }
            catch (RpcException rpcEx)
            {
                _logger.LogInformation($"RPC Exception. Export Failed. ${rpcEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Export Failed. ${ex.Message}");
            }
            return;
        }
    }
}