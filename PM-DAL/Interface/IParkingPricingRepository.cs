using PM_Common.DTO.Parking.Pricing;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IParkingPricingRepository : IRepositoryBase<ParkingPricing>
    {
        public Task AddParkingPricingInterval(Int64 parkingLotId, Int64 parkingPricingPlanId, short dayOfWeek, CancellationToken cancellationToken);
        public Task UpdateParkingPricingInterval(ParkingPricingDto pricingData, CancellationToken cancellationToken);
        public Task DeleteParkingPricingInterval(Int64 parkingPricingIntervalId, CancellationToken cancellationToken);
        public Task<Dictionary<short, IEnumerable<ParkingPricingDto>>> GetParkingPricingIntervals(Int64 parkingLotId, Int64 parkingPricingPlanId, CancellationToken cancellationToken);
    }
}
