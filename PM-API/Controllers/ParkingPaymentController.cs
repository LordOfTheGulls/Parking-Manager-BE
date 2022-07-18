using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO;
using PM_Common.DTO.Chart;
using PM_Common.DTO.Parking;
using PM_Common.DTO.Payment;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.Dashboard;
using PM_CQRS.Queries.Payment;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingPaymentController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher   _queryDispatcher;
        private readonly ILogger<ParkingPaymentController> _logger;

        public ParkingPaymentController(ILogger<ParkingPaymentController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("payforstay/{parkingLotId}")]
        [ProducesResponseType(typeof(OperationResult), 200)]
        public async Task<ActionResult> PayForStay(Int64 parkingLotId, [FromBody] ParkingPaymentDto payInfo, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<ParkingPayForStayCommand>(
                new ParkingPayForStayCommand()
                {
                    ParkingLotId   = parkingLotId,
                    LicensePlate   = payInfo.LicensePlate,
                    CreditCardInfo = payInfo.CreditCardInfo,
                }
            );
            return Ok();
        }

        [HttpPost("profit/week/{parkingLotId}")]
        [ProducesResponseType(typeof(WeeklyChartDto<double>), 200)]
        public async Task<ActionResult> GetTotalProfitForWeek(Int64 parkingLotId, [FromBody] ParkingPeriodDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetTotalProfitForWeek, WeeklyChartDto<double>>(
                new GetTotalProfitForWeek()
                {
                    ParkingLotId = parkingLotId,
                    Date         = filter.FromDate,
                }
            );
            return Ok(result);
        }

        [HttpPost("profit/period/{parkingLotId}")]
        [ProducesResponseType(typeof(double), 200)]
        public async Task<ActionResult> GetTotalProfitForPeriod(Int64 parkingLotId, [FromBody] ParkingPeriodDto filter, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetTotalProfitForPeriod, double>(
                new GetTotalProfitForPeriod()
                {
                    ParkingLotId = parkingLotId,
                    FromDate     = filter.FromDate,
                    ToDate       = filter.ToDate,
                }
            );
            return Ok(result);
        }
    }
}
