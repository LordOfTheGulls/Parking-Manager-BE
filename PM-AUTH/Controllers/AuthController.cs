using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PM_AUTH.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PM_AUTH.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(
            ILogger<AuthController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
        )
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userManager.CreateAsync(new IdentityUser()
            {
                UserName = "alex",
            }, "C@@l3rMast3r97");

            return Ok("Test");
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var v = await _userManager.FindByNameAsync(loginViewModel.Username);

                var res = await _signInManager.PasswordSignInAsync(loginViewModel.Username, "C@@l3rMast3r97", true, false);

                if (res.Succeeded)
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            return View();
        }


        /* [HttpGet("/")]
         public async Task<IActionResult> Token(
             string grant_type,
             string code,
             string redirect_uri,
             string client_id)
         {
             var claims = new[]
             {
                 new Claim(JwtRegisteredClaimNames.Sub, "ExampleUserId"),
             };

             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("just_an_example_of_a_secret_key"));

             var algorithm = SecurityAlgorithms.HmacSha256;

             var signingCredentials = new SigningCredentials(key, algorithm);

             //Move to appSettings
             var token = new JwtSecurityToken(
                 "https://localhost:7288",
                 "https://localhost:7288",
                 claims,
                 notBefore: DateTime.Now,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials
             );

             var access_token = new JwtSecurityTokenHandler().WriteToken(token);

             var responseObject = new
             {
                 access_token,
                 token_type = "Bearer",
                 raw_claim = "oauth"
             };

             var jsonResult = JsonConvert.SerializeObject(responseObject);

             var responseBytes = Encoding.UTF8.GetBytes(jsonResult);

             await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

             return Redirect(redirect_uri);
         }*/

        /*  [HttpGet]
          public IActionResult Authorize(
              string response_type, 
              string client_id, 
              string redirect_uri, 
              string scope, 
              string state)
          {
              var query = new QueryBuilder();
              query.Add("redirectUri", redirect_uri);
              query.Add("state", state);

              //return Redirect($"{redirect_uri}");
              return View();
          }

          [HttpPost]
          public IActionResult Authorize(
             string response_type,
             string redirect_uri,
             string state)
          {
              const string code = "Test";

              var query = new QueryBuilder();
              query.Add("code", code);
              query.Add("state", state);

              return Redirect($"{redirect_uri}{query.ToString()}");
          }

          */
    }
}