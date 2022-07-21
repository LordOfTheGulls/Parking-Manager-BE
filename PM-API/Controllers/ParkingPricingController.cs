using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Parking.Pricing;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.Parking;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingPricingController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ParkingPricingController> _logger;

        public ParkingPricingController(ILogger<ParkingPricingController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger            = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher   = queryDispatcher;
        }

        [HttpPost("interval/add/{parkingLotId}")]
        public async Task<ActionResult> AddParkingPricingInterval(Int64 parkingLotId, [FromBody] AddParkingPricingIntervalDto parkingPricing, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<AddParkingPricingIntervalCommand>(
                new AddParkingPricingIntervalCommand()
                {
                    ParkingLotId         = parkingLotId,
                    ParkingPricingPlanId = parkingPricing.ParkingPricingPlanId,
                    DayOfWeek            = parkingPricing.DayOfWeek
                }
            );
            return Ok();
        }

        [HttpPost("interval/update/{parkingPricingIntervalId}")]
        public async Task<ActionResult> UpdateParkingPricingInterval(Int64 parkingPricingIntervalId, [FromBody] ParkingPricingDto pricingData, CancellationToken token = default)
        {
            pricingData.ParkingPricingIntervalId = parkingPricingIntervalId;

            await _commandDispatcher.DispatchAsync<UpdateParkingPricingIntervalCommand>(
                new UpdateParkingPricingIntervalCommand()
                {
                    PricingData = pricingData
                }
            );
            return Ok();
        }

        [HttpPost("interval/delete/{parkingPricingIntervalId}")]
        public async Task<ActionResult> DeleteParkingPricingInterval(Int64 parkingPricingIntervalId, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<DeleteParkingPricingIntervalCommand>(
                new DeleteParkingPricingIntervalCommand(){
                    PricingIntervalId = parkingPricingIntervalId
                }
            );
            return Ok();
        }
        
        [HttpGet("intervals/{parkingLotId}/{parkingPricingPlanId}")]
        [ProducesResponseType(typeof(Dictionary<short, IEnumerable<ParkingPricingDto>>), 200)]
        public async Task<ActionResult> GetParkingPricingIntervals(Int64 parkingLotId, Int64 parkingPricingPlanId, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetParkingPricingIntervals, Dictionary<short, IEnumerable<ParkingPricingDto>>>(
                new GetParkingPricingIntervals(){
                    ParkingLotId         = parkingLotId,
                    ParkingPricingPlanId = parkingPricingPlanId,
                }
            );
            return Ok(result);
        }

    }
}
