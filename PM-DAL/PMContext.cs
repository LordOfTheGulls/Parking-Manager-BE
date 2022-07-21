using Microsoft.EntityFrameworkCore;
using PM_DAL.Interface;
using PM_DAL.Interfaces;
using PM_DAL.Repository;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL
{
    public interface IDbContext<T> where T: IUnitOfWork
    {
        public Task<T> OpenAsync();
    }

    public class PMContext : IDbContext<IUnitOfWork>
    {
        private static readonly Dictionary<Type, Func<IUnitOfWork, IRepository>> _repoAccessors = new Dictionary<Type, Func<IUnitOfWork, IRepository>>
        {
             {typeof(ParkingLotRepository),      uow => uow.ParkingLotRepository },
             {typeof(ParkingEventRepository),    uow => uow.ParkingEventRepository },
             {typeof(ParkingEventLogRepository), uow => uow.ParkingEventLogRepository },
             {typeof(ParkingTrafficRepository),  uow => uow.ParkingTrafficRepository },
             {typeof(ParkingPaymentRepository),  uow => uow.ParkingPaymentRepository },
             {typeof(ParkingPaymentMethodRepository),  uow => uow.ParkingPaymentMethodRepository },
             {typeof(ParkingWorkhoursRepository),       uow => uow.ParkingWorkhoursRepository },
             {typeof(ParkingWorkhoursPlanRepository),  uow => uow.ParkingWorkhoursPlanRepository },
        };

        private readonly Func<bool, PMDBContext> _contextFactory;

        public static DbContextOptionsBuilder<TContext> ConfigureDbContextOptionsBuilder<TContext>(string connectionString) where TContext : DbContext
        {
            var optionBuilder = new DbContextOptionsBuilder<TContext>();

            ConfigureDbContextOptionsBuilder(connectionString, optionBuilder);

            return optionBuilder;
        }

        public static void ConfigureDbContextOptionsBuilder(string connectionString, DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected PMContext(Func<bool, PMDBContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public PMContext(string connectionString): this(_ => new PMDBContext(ConfigureDbContextOptionsBuilder<PMDBContext>(connectionString).Options))
        {

        }

        public async Task<IUnitOfWork> OpenAsync()
        {
            return await Open();
        }

        private async Task<IUnitOfWork> Open()
        {
            var uof = new UnitOfWork(_contextFactory(false));

            try
            {
                await uof.OpenConnectionAsync();
            }
            catch
            {
                uof.Dispose();
                throw;
            }

            return uof;
        }
    }
}
