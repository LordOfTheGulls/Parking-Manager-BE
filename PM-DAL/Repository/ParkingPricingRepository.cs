using Microsoft.EntityFrameworkCore;
using PM_Common.DTO.Parking.Pricing;
using PM_Common.Exceptions;
using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class ParkingPricingRepository : RepositoryBase<ParkingPricing>, IParkingPricingRepository
    {
        private readonly PMDBContext context;

        public ParkingPricingRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddParkingPricingInterval(Int64 parkingLotId, Int64 parkingPricingPlanId, short dayOfWeek, CancellationToken cancellationToken)
        {
            ParkingPricing? pricing = new ParkingPricing()
            {
                ParkingLotId  = parkingLotId,
                PricingPlanId = parkingPricingPlanId,
                DayOfWeek     = dayOfWeek
            };

            await context.ParkingPricing.AddAsync(pricing);
        }

        public async Task UpdateParkingPricingInterval(ParkingPricingDto pricingData, CancellationToken cancellationToken)
        {
            ParkingPricing? pricing = await context.ParkingPricing.FirstOrDefaultAsync(p => p.Id == pricingData.ParkingPricingIntervalId, cancellationToken);

            if (pricing == null)
            {
                throw new EntityDoesNotExistException(pricingData.ParkingPricingIntervalId, typeof(ParkingPricing));
            }

            pricing.Incremental = pricingData.Incremental;
            pricing.IncrementalRate = pricingData.IncrementalRate;
            pricing.IntervalStart = pricingData.IntervalStart;
            pricing.IntervalEnd = pricingData.IntervalEnd;
            pricing.Rate = pricingData.Rate;
        }

        public async Task DeleteParkingPricingInterval(Int64 parkingPricingIntervalId, CancellationToken cancellationToken)
        {
            ParkingPricing? pricing = await context.ParkingPricing.FirstOrDefaultAsync(p => p.Id == parkingPricingIntervalId, cancellationToken);

            if (pricing == null)
            {
                throw new EntityDoesNotExistException(parkingPricingIntervalId, typeof(ParkingPricing));
            }

            context.ParkingPricing.Remove(pricing);
        }

        public async Task<Dictionary<short, IEnumerable<ParkingPricingDto>>> GetParkingPricingIntervals(Int64 parkingLotId, Int64 parkingPricingPlanId, CancellationToken cancellationToken)
        {
            var query = context.ParkingPricing
                         .Where(p => p.ParkingLotId == parkingLotId && p.PricingPlanId == parkingPricingPlanId)
                         .GroupBy(p => p.DayOfWeek)
                         .Select(t => new { Key = t.Key, Value = t.ToList() });

            return await query.ToDictionaryAsync(p => p.Key, p => p.Value.OrderBy(p => p.Id).Select(p => new ParkingPricingDto()
            {
                ParkingPricingIntervalId = p.Id,
                Incremental = p.Incremental.GetValueOrDefault(),
                IncrementalRate = p.IncrementalRate.GetValueOrDefault(),
                IntervalStart = p.IntervalStart.GetValueOrDefault(),
                IntervalEnd = p.IntervalEnd.GetValueOrDefault(),
                Rate = p.Rate.GetValueOrDefault(),
            }), cancellationToken);

        }
    }
}
