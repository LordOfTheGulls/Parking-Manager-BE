using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services
.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", o =>
{
    o.Authority = "https://localhost:7288";
    o.Audience  = "pmapi";
    o.RequireHttpsMetadata = false;
});

builder.Services
.AddAuthorization();

/*builder.Services
.AddAuthentication(config =>
{
    //We check the cooked to confirm we are authenticated.
    config.DefaultAuthenticateScheme = "ClientCookie";
    //When we sign in we will deal out a cookie.
    config.DefaultSignInScheme = "ClientCookie";
    //Use this to check if we are allowed to do something.
    config.DefaultChallengeScheme = "Server";
})
.AddCookie("ClientCookie")
.AddOAuth("Server", config =>
{
    config.ClientId = "client_id";
    config.ClientSecret = "client_secret";
    config.CallbackPath = "/oauth/callback";
    config.AuthorizationEndpoint = "https://localhost:7288/accounts/login"; //Server Path
    config.TokenEndpoint         = "https://localhost:7288/auth/token"; //Server Path
    config.SaveTokens = true;
    config.Events = new OAuthEvents()
    {
        OnCreatingTicket = context =>
        {
            var accessToken = context.AccessToken;
            
            var payload = accessToken.Split('.')[1];
            
            var bytes = Convert.FromBase64String(payload);
            
            var jsonPayload = Encoding.UTF8.GetString(bytes);
            
            var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);
            
            foreach(var claim in claims)
            {
                context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
            }

            return Task.CompletedTask;
        }
    };
});*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
