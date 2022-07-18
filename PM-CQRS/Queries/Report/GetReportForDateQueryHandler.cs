using Microsoft.Extensions.Logging;
using PM_Common.DTO.Report;
using PM_Common.Enums.Report;
using PM_Common.Exceptions;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Report
{
    public class GetReportForDateQueryHandler : IQueryHandler<GetReportForDate, ReportResultDto>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<GetReportForDateQueryHandler> _logger;

        public GetReportForDateQueryHandler(ILogger<GetReportForDateQueryHandler> logger, IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ReportResultDto> HandleAsync(GetReportForDate query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            ReportType? reportType = query.ReportByDateFilter.ReportType;

            try
            {
                if (reportType != null || Enum.IsDefined(typeof(ReportType), (int)reportType))
                {
                    switch (reportType)
                    {
                        case ReportType.ParkingEventReport:
                            {
                                return await uow.ParkingEventLogRepository.GetParkingEventReportByDate(query.ReportByDateFilter, token);
                            }
                        case ReportType.ParkingTrafficReport:
                            {
                                return null;
                            }
                        case ReportType.ParkingPaymentReport:
                            {
                                return null;
                            }
                        case ReportType.UserActivityReport:
                            {
                                return null;
                            }
                        default: return null;
                    }
                }
                else
                {
                    throw new ValidationFailedException("Report Type is not specified or is incorrect!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetReportForDateQueryHandler Failed! Error: {ex.Message}");
                return null;
            }
        }
    }
}
