using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries;
using PM_DAL.UOW;

namespace PM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        public readonly ICommandDispatcher _commandDispatcher;
        public readonly IQueryDispatcher _queryDispatcher;
        public readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger, ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher   = queryDispatcher;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

           var v = await HttpContext.GetTokenAsync("access_token");

           var result = _queryDispatcher.DispatchAsync<GetParkingSpotStatusById, string>(
                new GetParkingSpotStatusById() { 
                    ParkingLotId = 1, 
                    ParkingSpotId = 1
                }
            );

            return Ok("Hello");
        }

        [Authorize]
        [HttpGet("Secret")]
        public IActionResult Secret()
        {
            return Ok("You have received access to the api!");
        }
    }
}