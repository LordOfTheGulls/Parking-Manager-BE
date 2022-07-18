

using PM_Common.DTO.Paging;
using PM_Common.DTO.Report;

namespace PM_CQRS.Queries.Report
{
    public class GetReportCountByFilter : IQuery<PagingResult<ReportCountResultDto>>
    {
        public ReportCountFilterDto ReportFilter { get; set; }
    }
}
