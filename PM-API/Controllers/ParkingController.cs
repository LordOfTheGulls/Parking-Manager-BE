using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO;
using PM_Common.DTO.Parking;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.Parking;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ParkingController> _logger;

        public ParkingController(ILogger<ParkingController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("location/update/{parkingLotId}")]
        public async Task<ActionResult> UpdateParkingLocation(Int64 parkingLotId, [FromBody] ParkingLocationDto location, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<UpdateParkingLotLocationCommand>(
                new UpdateParkingLotLocationCommand()
                {
                    ParkingLotId = parkingLotId,
                    ParkingLocation = location,
                }
            );
            return Ok();
        }


        [HttpGet("{parkingLotId}")]
        [ProducesResponseType(typeof(ParkingLotDto), 200)]
        public async Task<ActionResult> GetParkingInfo(Int64 parkingLotId, CancellationToken token = default)
        {
            var info = await _queryDispatcher.DispatchAsync<GetParkingLotInfoQuery, ParkingLotDto>(
                new GetParkingLotInfoQuery()
                {
                    ParkingLotId = parkingLotId,
                }
            );
            return Ok(info);
        }
    }
}
