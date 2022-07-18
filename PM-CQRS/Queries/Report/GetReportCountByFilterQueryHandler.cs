using Microsoft.Extensions.Logging;
using PM_Common.DTO.Paging;
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
    public class GetReportCountByFilterQueryHandler : IQueryHandler<GetReportCountByFilter, PagingResult<ReportCountResultDto>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<GetReportCountByFilterQueryHandler> _logger;

        public GetReportCountByFilterQueryHandler(ILogger<GetReportCountByFilterQueryHandler> logger, IDbContext<IUnitOfWork> uow)
        {
            _uow    = uow    ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagingResult<ReportCountResultDto>> HandleAsync(GetReportCountByFilter query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            ReportType? reportType = query.ReportFilter.ReportType;

            try
            {
                if (reportType != null || Enum.IsDefined(typeof(ReportType), (int)reportType))
                {
                    switch (reportType)
                    {
                        case ReportType.ParkingEventReport:
                        {
                           return await uow.ParkingEventLogRepository.GetParkingEventReportCounts(query.ReportFilter, token);
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
            }catch(Exception ex)
            {
                _logger.LogError($"GetReportCountByFilter Failed! Error: {ex.Message}");
                return null;
            }
        }
    }
}
