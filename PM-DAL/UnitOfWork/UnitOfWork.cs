using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PM_DAL.Interface;
using PM_DAL.Interfaces;
using PM_DAL.Repository;
using System.Data;
using System.Data.Common;

namespace PM_DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;

        private readonly PMDBContext _context;

        public IUserRepository UserRepository { get; private set; }

        public IUserRoleRepository UserRoleRepository { get; private set; }

        public IUserClaimRepository UserClaimRepository { get; private set; }

        public IUserLoginRepository UserLoginRepository { get; private set; }

        public IUserTokenRepository UserTokenRepository { get; private set; }


        public IRoleRepository RoleRepository { get; private set; }

        public IRoleClaimRepository RoleClaimRepository { get; private set; }


        public IParkingLotRepository ParkingLotRepository { get; private set; }

        public IParkingLotPaymentMethodRepository ParkingLotPaymentMethodRepository { get; private set; }

        public IPaymentMethodRepository PaymentMethodRepository { get; private set; }

        public IParkingSpotRepository ParkingSpotRepository { get; private set; }

        public IParkingSpotTypeRepository ParkingSpotTypeRepository { get; private set; }

        public IParkingFloorRepository ParkingFloorRepository { get; private set; }


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
            ParkingLotPaymentMethodRepository = new ParkingLotPaymentMethodRepository(_context);
            PaymentMethodRepository           = new PaymentMethodRepository(_context);
            ParkingSpotRepository             = new ParkingSpotRepository(_context);
            ParkingSpotTypeRepository         = new ParkingSpotTypeRepository(_context);
            ParkingFloorRepository            = new ParkingFloorRepository(_context);
        }

        public async Task Initialize()
        {
            _connection = _context.Database.GetDbConnection();

            if(_connection.State != ConnectionState.Open)
            {
                await _context.Database.OpenConnectionAsync();
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }
    }
}
