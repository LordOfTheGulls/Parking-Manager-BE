using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Parking.Blacklist;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.Parking;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingBlacklistController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<ParkingBlacklistController> _logger;

        public ParkingBlacklistController(ILogger<ParkingBlacklistController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost("add/{parkingLotId}")]
        public async Task<ActionResult> AddToParkingBlacklist(Int64 parkingLotId, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<AddParkingBlacklistCommand>(
                new AddParkingBlacklistCommand()
                {
                    ParkingLotId = parkingLotId,
                }
            );
            return Ok();
        }

        [HttpPost("update/{parkingLotId}")]
        public async Task<ActionResult> UpdateParkingBlacklist(Int64 parkingLotId, [FromBody] ParkingBlacklistDto parkingBlacklist, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<UpdateParkingBlacklist>(
                new UpdateParkingBlacklist()
                {
                    ParkingLotId = parkingLotId,
                    ParkingBlacklist = parkingBlacklist,
                }
            );
            return Ok();
        }

        [HttpPost("delete/{parkingLotId}/{parkingBlackListId}")]
        public async Task<ActionResult> DeleteParkingBlacklist(Int64 parkingLotId, Int64 parkingBlackListId, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<DeleteParkingBlacklistCommand>(
                new DeleteParkingBlacklistCommand()
                {
                   ParkingBlackListId = parkingBlackListId,
                }
            );
            return Ok();
        }

        [HttpPost("all/{parkingLotId}")]
        [ProducesResponseType(typeof(List<ParkingBlacklistDto>), 200)]
        public async Task<ActionResult> GetParkingBlacklist(Int64 parkingLotId, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetParkingBlacklist, List<ParkingBlacklistDto>>(
                new GetParkingBlacklist(){ ParkingLotId = parkingLotId }
            );
            return Ok(result);
        }
    }
}
