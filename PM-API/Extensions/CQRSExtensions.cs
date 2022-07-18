using PM_CQRS.Commands;
using PM_CQRS.Queries;

namespace PM_API.Extensions
{
    public static class CQRSExtensions
    {
        public static void AddCommandQueryHandlers(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = typeof(Program).Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }
    }
}
