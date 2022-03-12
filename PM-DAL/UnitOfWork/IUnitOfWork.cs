using PM_DAL.Interface;
using PM_DAL.Interfaces;
using PM_DAL.Repository;

namespace PM_DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IUserClaimRepository UserClaimRepository { get; }
        public IUserLoginRepository UserLoginRepository { get; }
        public IUserTokenRepository UserTokenRepository { get; }

        public IRoleRepository RoleRepository { get; }
        public IRoleClaimRepository RoleClaimRepository { get; }

        public IParkingLotRepository ParkingLotRepository { get; }
        public IParkingLotPaymentMethodRepository ParkingLotPaymentMethodRepository { get; }
        public IPaymentMethodRepository PaymentMethodRepository { get; }
        public IParkingSpotRepository ParkingSpotRepository { get; }
        public IParkingSpotTypeRepository ParkingSpotTypeRepository { get; }
        public IParkingFloorRepository ParkingFloorRepository { get; }
    }
}
