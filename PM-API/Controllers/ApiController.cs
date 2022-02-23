using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {

           var v = await HttpContext.GetTokenAsync("access_token");
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