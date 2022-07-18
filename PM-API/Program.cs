using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PM_API.Extensions;
using PM_API.Extensions.Services;
using PM_API.Middlewares;
using PM_API.Services;
using PM_Common.DTO.Chart;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Event;
using PM_Common.DTO.Parking.Traffic;
using PM_Common.DTO.Report;
using PM_CQRS.Commands;
using PM_CQRS.Commands.PM_CQRS.Commands;
using PM_CQRS.Dispatcher;
using PM_CQRS.Queries;
using PM_CQRS.Queries.Dashboard;
using PM_CQRS.Queries.Parking;
using PM_CQRS.Queries.Payment;
using PM_CQRS.Queries.Report;
using PM_DAL;
using PM_DAL.UOW;
using System.IO.Compression;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

//Services.
builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", o =>
{
    o.Authority = "https://localhost:7288";
    o.Audience  = "pmapi";
    o.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization();

builder.Services.AddGrpc().AddServiceOptions<ExportService>(o =>
{
    o.ResponseCompressionLevel = CompressionLevel.Optimal;
    o.ResponseCompressionAlgorithm = "gzip";
}); 

//builder.Services.AddRaspberryWebSocketClient();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Injects Database's Context as a Scoped Service.
builder.Services.AddScoped<IDbContext<IUnitOfWork>, PMContext>(p => new PMContext(
        builder.Configuration.GetConnectionString("PostgreSQL")
));

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddScoped<IQueryHandler<GetParkingSpotStatusById, string>, GetParkingSpotStatusByIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetParkingEventsByLotId, PagingResult<ParkingEventDTO>>, GetParkingEventsByLotIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetParkingTrafficByLotId, PagingResult<ParkingTrafficDTO>>, GetParkingTrafficByLotIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetParkingTrafficInForWeek, WeeklyChartDto<int>>, GetParkingTrafficInForWeekQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetTotalParkingTrafficInForPeriod, int>, GetTotalParkingTrafficInForPeriodQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetAverageStayTimeForPeriod, double>, GetAverageStayTimeForPeriodQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetTotalProfitForPeriod, double>, GetTotalProfitForPeriodQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetTotalProfitForWeek, WeeklyChartDto<double>>, GetTotalProfitForWeekQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetTotalParkingEventsForWeek, WeeklyChartDto<int>>, GetTotalParkingEventsForWeekQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetReportCountByFilter, PagingResult<ReportCountResultDto>>, GetReportCountByFilterQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetReportForDate, ReportResultDto>, GetReportForDateQueryHandler>();
builder.Services.AddScoped<ICommandHandler<LogParkingEventCommand>, LogParkingEventCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateParkingLotLocationCommand>, UpdateParkingLotLocationHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateOnParkingEmitCommand>, UpdateOnParkingEmitCommandHandler>();

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

builder.Services.AddWebSocketManager();
builder.Services
.AddControllers()
.AddFluentValidation(o => {
    o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}).AddNewtonsoftJson(o => {
    o.SerializerSettings.ContractResolver = new DefaultContractResolver()
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate((o) => { });

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseRouting();
app.UseGrpcWeb();
app.UseCors("AllowAll");

app.MapGrpcService<ParkingService>();

app.UseEndpoints(endpoints =>
    endpoints.MapGrpcService<ExportService>().EnableGrpcWeb().RequireCors("AllowAll")
);

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
