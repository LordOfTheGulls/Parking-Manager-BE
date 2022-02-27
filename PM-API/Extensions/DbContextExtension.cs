using Microsoft.EntityFrameworkCore;
using PM_DAL;
using PM_DAL.UnitOfWork;

namespace PM_API.Extensions.Services
{
    public static class DbContextExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PMDBContext>(o => o.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}