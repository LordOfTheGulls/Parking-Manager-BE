using PM_Common.DTO.Parking.Pricing;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingPricingIntervalsQueryHandler : IQueryHandler<GetParkingPricingIntervals, Dictionary<short, IEnumerable<ParkingPricingDto>>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingPricingIntervalsQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Dictionary<short, IEnumerable<ParkingPricingDto>>> HandleAsync(GetParkingPricingIntervals query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            var result = await uow.ParkingPricingRepository.GetParkingPricingIntervals(query.ParkingLotId, query.ParkingPricingPlanId, token);

            await uow.CommitAsync();

            return result;
        }
    }
}
