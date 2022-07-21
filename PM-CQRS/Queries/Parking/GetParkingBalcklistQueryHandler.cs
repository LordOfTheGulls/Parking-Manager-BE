using PM_Common.DTO.Parking.Blacklist;
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
    public class GetParkingBlacklistQueryHandler : IQueryHandler<GetParkingBlacklist, List<ParkingBlacklistDto>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingBlacklistQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<List<ParkingBlacklistDto>> HandleAsync(GetParkingBlacklist query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            var result = await uow.ParkingLotBlacklistRepository.GetParkingBlacklist(query.ParkingLotId, token);

            return result;
        }
    }
}
