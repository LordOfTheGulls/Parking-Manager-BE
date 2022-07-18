using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Chart;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking;
using PM_Common.DTO.Parking.Event;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries;
using PM_CQRS.Queries.Dashboard;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingEventController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher    _queryDispatcher;
        private readonly ILogger<ParkingEventController> _logger;
        public ParkingEventController(ILogger <ParkingEventController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher   = queryDispatcher;
        }

        [HttpPost("week/{parkingLotId}")]
        [ProducesResponseType(typeof(WeeklyChartDto<int>), 200)]
        public async Task<ActionResult> GetParkingTrafficInForWeek(Int64 parkingLotId, [FromBody] ParkingPeriodDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetTotalParkingEventsForWeek, WeeklyChartDto<int>>(
                new GetTotalParkingEventsForWeek()
                {
                    ParkingLotId = parkingLotId,
                    Date         = filter.FromDate
                }
            );
            return Ok(result);
        }

        [HttpPost("all/{parkingLotId}")]
        [ProducesResponseType(typeof(PagingResult<ParkingEventDTO>), 200)]
        public async Task<ActionResult> GetParkingLotEventsPaginated(Int64 parkingLotId, [FromBody] FilterDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetParkingEventsByLotId, PagingResult<ParkingEventDTO>>(
                new GetParkingEventsByLotId(){ LotId = parkingLotId, Filter = filter }
            );
            return Ok(result);
        }
    }
}
