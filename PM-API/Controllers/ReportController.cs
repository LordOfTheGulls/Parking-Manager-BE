using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Report;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.Report;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ReportController> _logger;
        public ReportController(ILogger<ReportController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("bydate/{lotId}")]
        [ProducesResponseType(typeof(ReportResultDto), 200)]
        public async Task<ActionResult> GetReportsForDate(Int64 lotId, [FromBody] ReportByDateFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.DispatchAsync<GetReportForDate, ReportResultDto>(
                 new GetReportForDate(){
                     ReportByDateFilter = new ReportByDateFilterDto()
                     {
                         ParkingLotId = lotId,
                         ReportType   = filter.ReportType,
                         ForDate      = filter.ForDate,
                     }
                 });
            return Ok(result);
        }

        [HttpPost("counts/{lotId}")]
        [ProducesResponseType(typeof(PagingResult<ReportCountResultDto>), 200)]
        public async Task<ActionResult> GetReportsCountByDate(Int64 lotId, [FromBody] ReportCountFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _queryDispatcher.DispatchAsync<GetReportCountByFilter, PagingResult<ReportCountResultDto>>(
                 new GetReportCountByFilter() { 
                     ReportFilter = new ReportCountFilterDto()
                     {
                         ParkingLotId   = lotId,
                         Filter         = filter.Filter,
                         FromDate       = filter.FromDate,
                         ToDate         = filter.ToDate,
                         ReportType     = filter.ReportType,
                     }
                });
            return Ok(result);
        }
    }
}
