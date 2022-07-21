using Microsoft.AspNetCore.Mvc;
using PM_Common.DTO.Parking.Workhours;
using PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries.Parking;

namespace PM_API.Controllers
{
    [Route("[controller]")]
    public class ParkingWorkhoursController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher   _queryDispatcher;

        private readonly ILogger<ParkingWorkhoursController> _logger;

        public ParkingWorkhoursController(ILogger<ParkingWorkhoursController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base()
        {
            _logger            = logger;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher   = queryDispatcher;
        }

        [HttpPost("all/{parkingLotId}")]
        [ProducesResponseType(typeof(List<WorkhoursDTO>), 200)]
        public async Task<ActionResult> GetParkingWorkhours(Int64 parkingLotId, [FromBody] Int64 parkingWorkhoursPlanId, CancellationToken token = default)
        {
            var result = await _queryDispatcher.DispatchAsync<GetParkingWorkhours, List<WorkhoursDTO>>(
                new GetParkingWorkhours()
                {
                    ParkingLotId    = parkingLotId,
                    WorkHoursPlanId = parkingWorkhoursPlanId,
                }
            );
            return Ok(result);
        }

        [HttpPost("update/{parkingLotId}")]
        public async Task<ActionResult> UpdateParkingWorkhours(Int64 parkingLotId, [FromBody] UpdateWorkhoursDTO parkingWorkhours, CancellationToken token = default)
        {
            await _commandDispatcher.DispatchAsync<UpdateParkingWorkhoursCommand>(
                new UpdateParkingWorkhoursCommand()
                {
                    ParkingLotId           = parkingLotId,
                    Workhours              = new UpdateWorkhoursDTO()
                    {
                        OpenTime              = parkingWorkhours.OpenTime,
                        CloseTime             = parkingWorkhours.CloseTime,
                        ParkingWorkhourPlanId = parkingWorkhours.ParkingWorkhourPlanId,
                        WorkhourId            = parkingWorkhours.WorkhourId
                    }
                }
            );
            return Ok();
        }
    }
}
