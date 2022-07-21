using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PM_DAL.Interface;
using PM_DAL.Interfaces;
using PM_DAL.Repository;
using System.Data;
using System.Data.Common;

namespace PM_DAL.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public DbConnection _connection { get; private set; }

        private readonly PMDBContext _context;

        /*** User Related ***/
        public IUserRepository UserRepository { get; private set; }

        public IUserRoleRepository UserRoleRepository { get; private set; }

        public IUserClaimRepository UserClaimRepository { get; private set; }

        public IUserLoginRepository UserLoginRepository { get; private set; }

        public IUserTokenRepository UserTokenRepository { get; private set; }

        public IRoleRepository RoleRepository { get; private set; }

        public IRoleClaimRepository RoleClaimRepository { get; private set; }


        /*** Parking Lot Related ***/
        public IParkingLotRepository ParkingLotRepository { get; private set; }
        public IParkingLotTypeRepository ParkingLotTypeRepository { get; private set; }


        public IParkingLotBlacklistRepository ParkingLotBlacklistRepository { get; private set; }


        public IParkingEventRepository ParkingEventRepository { get; private set; }
        public IParkingEventLogRepository ParkingEventLogRepository { get; private set; }

        public IParkingTrafficRepository ParkingTrafficRepository { get; private set; }

        public IParkingPricingPlanRepository ParkingPricingPlanRepository { get; private set; }
        public IParkingPricingRepository ParkingPricingRepository { get; private set; }

        public IParkingPaymentRepository ParkingPaymentRepository { get; private set; }


        public IParkingPaymentMethodRepository ParkingPaymentMethodRepository { get; private set; }

        public IParkingSpotRepository ParkingSpotRepository { get; private set; }

        public IParkingSpotTypeRepository ParkingSpotTypeRepository { get; private set; }

        public IParkingFloorRepository ParkingFloorRepository { get; private set; }

        public IParkingWorkhoursRepository ParkingWorkhoursRepository { get; private set; }
        public IParkingWorkhoursPlanRepository ParkingWorkhoursPlanRepository { get; private set; }


        private IDbContextTransaction Transaction;

        private bool _disposed;

        public UnitOfWork(PMDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            UserRepository                    = new UserRepository(_context);
            UserRoleRepository                = new UserRoleRepository(_context);
            UserClaimRepository               = new UserClaimRepository(_context);
            UserLoginRepository               = new UserLoginRepository(_context);
            UserTokenRepository               = new UserTokenRepository(_context);

            RoleRepository                    = new RoleRepository(_context);
            RoleClaimRepository               = new RoleClaimRepository(_context);

            ParkingLotRepository              = new ParkingLotRepository(_context);
            ParkingLotTypeRepository          = new ParkingLotTypeRepository(_context);

            ParkingEventRepository            = new ParkingEventRepository(_context); 
            ParkingEventLogRepository         = new ParkingEventLogRepository(_context);

            ParkingTrafficRepository          = new ParkingTrafficRepository(_context);

            ParkingPricingPlanRepository      = new ParkingPricingPlanRepository(_context);
            ParkingPricingRepository          = new ParkingPricingRepository(_context);

            ParkingPaymentRepository = new ParkingPaymentRepository(_context);
            ParkingPaymentMethodRepository    = new ParkingPaymentMethodRepository(_context);

            ParkingSpotRepository             = new ParkingSpotRepository(_context);
            ParkingSpotTypeRepository         = new ParkingSpotTypeRepository(_context);
            ParkingFloorRepository            = new ParkingFloorRepository(_context);

            ParkingLotBlacklistRepository     = new ParkingLotBlacklistRepository(_context);

            ParkingWorkhoursRepository        = new ParkingWorkhoursRepository(_context);
            ParkingWorkhoursPlanRepository    = new ParkingWorkhoursPlanRepository(_context);
        }

        public async Task OpenConnectionAsync()
        {
            _connection = _context.Database.GetDbConnection();

            if (_connection.State != ConnectionState.Open)
            {
                await _context.Database.OpenConnectionAsync();
            }

            Transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();

            Transaction.Commit();
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Transaction?.Dispose();

                    if(_connection != null)
                    {
                        _context.Database.CloseConnection();
                        _connection.Dispose();
                    }

                    _context.Dispose();
                }
                _disposed = true;
            }
        }

    }
}
