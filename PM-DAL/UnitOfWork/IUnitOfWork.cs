using PM_DAL.Interface;
using PM_DAL.Interfaces;
using PM_DAL.Repository;
using System.Data.Common;

namespace PM_DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        public Task OpenConnectionAsync();
        public Task CommitAsync();

        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IUserClaimRepository UserClaimRepository { get; }
        public IUserLoginRepository UserLoginRepository { get; }
        public IUserTokenRepository UserTokenRepository { get; }

        public IRoleRepository RoleRepository { get; }
        public IRoleClaimRepository RoleClaimRepository { get; }

        public IParkingLotRepository ParkingLotRepository { get; }
        public IParkingLotBlacklistRepository ParkingLotBlacklistRepository { get; }

        public IParkingEventRepository ParkingEventRepository { get; }
        public IParkingEventLogRepository ParkingEventLogRepository { get; }

        public IParkingTrafficRepository ParkingTrafficRepository { get; }

        public IParkingPricingPlanRepository ParkingPricingPlanRepository { get; }
        public IParkingPricingRepository ParkingPricingRepository { get; }

        public IParkingPaymentRepository ParkingPaymentRepository { get; }

        public IParkingPaymentMethodRepository ParkingPaymentMethodRepository { get; }

        public IParkingSpotRepository ParkingSpotRepository { get; }
        public IParkingSpotTypeRepository ParkingSpotTypeRepository { get; }
        public IParkingFloorRepository ParkingFloorRepository { get; }

        public IParkingWorkhoursPlanRepository ParkingWorkhoursPlanRepository { get; }
        public IParkingWorkhoursRepository ParkingWorkhoursRepository { get; }
    }
}
