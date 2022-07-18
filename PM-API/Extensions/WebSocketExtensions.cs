using PM_API.Services;

namespace PM_API.Middlewares
{
    public static class WebSocketExtensions
    {
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketMiddleware>();
        }

        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketServerManager>();

            services.AddSingleton<ParkingSocketManager>();

            return services;
        }
    }
}
