using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Chart;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking;
using PM_Common.DTO.Parking.Event;
using PM_Common.DTO.Parking.Traffic;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries;
using PM_CQRS.Queries.Dashboard;
using PM_CQRS.Queries.Parking;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingTrafficController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ParkingTrafficController> _logger;
        public ParkingTrafficController(ILogger<ParkingTrafficController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("all/{parkingLotId}")]
        [ProducesResponseType(typeof(PagingResult<ParkingTrafficDTO>), 200)]
        public async Task<ActionResult> GetParkingTrafficPaginated(Int64 parkingLotId, [FromBody] FilterDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetParkingTrafficByLotId, PagingResult<ParkingTrafficDTO>>(
                new GetParkingTrafficByLotId()
                {
                    LotId = parkingLotId,
                    Filter = filter
                }
            );
            return Ok(result);
        }

        [HttpPost("in/period/{parkingLotId}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<ActionResult> GetTotalParkingTrafficInByPeriod(Int64 parkingLotId, [FromBody] ParkingTrafficInTotalDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetTotalParkingTrafficInForPeriod, int>(
                new GetTotalParkingTrafficInForPeriod()
                {
                    ParkingLotId = parkingLotId,
                    FromDate     = filter.FromDate,
                    ToDate       = filter.ToDate
                }
            );
            return Ok(result);
        }

        [HttpPost("in/week/{parkingLotId}")]
        [ProducesResponseType(typeof(WeeklyChartDto<int>), 200)]
        public async Task<ActionResult> GetParkingTrafficInForWeek(Int64 parkingLotId, [FromBody] ParkingTrafficInForWeekDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetParkingTrafficInForWeek, WeeklyChartDto<int>>(
                new GetParkingTrafficInForWeek() { 
                    ParkingLotId = parkingLotId, 
                    Date = filter.Date
                }
            );
            return Ok(result);
        }

        [HttpPost("stay/average/period/{parkingLotId}")]
        [ProducesResponseType(typeof(double), 200)]
        public async Task<ActionResult> GetAverageStayTimeForPeriod(Int64 parkingLotId, [FromBody] ParkingPeriodDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetAverageStayTimeForPeriod, double>(
                new GetAverageStayTimeForPeriod()
                {
                    ParkingLotId = parkingLotId,
                    FromDate     = filter.FromDate,
                    ToDate       = filter.ToDate
                }
            );
            return Ok(result);
        }
    }
}
