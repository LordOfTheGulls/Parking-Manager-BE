using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM_DAL.UnitOfWork;

namespace PM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private IUnitOfWork Uow {get; set;}

        public ApiController(IUnitOfWork uow)
        {
            Uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

           var v = await HttpContext.GetTokenAsync("access_token");

          // Console.WriteLine(await Uow.ParkingLotRepository.GetAll());

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