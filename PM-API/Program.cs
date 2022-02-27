using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PM_API.Extensions.Services;
using PM_API.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Services.
builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", o =>
{
    o.Authority = "https://localhost:7288";
    o.Audience  = "pmapi";
    o.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization();

builder.Services.AddWebSocketManager();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddControllers()
.AddFluentValidation(o => {
    o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
})
.AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
});

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

app.UseWebSockets(new WebSocketOptions()
{
    KeepAliveInterval = TimeSpan.FromSeconds(5500),
});

app.UseWebSocketServer();

app.MapControllers();

app.Run();
