using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Traffic;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingTrafficByLotIdQueryHandler : IQueryHandler<GetParkingTrafficByLotId, PagingResult<ParkingTrafficDTO>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingTrafficByLotIdQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<PagingResult<ParkingTrafficDTO>> HandleAsync(GetParkingTrafficByLotId query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingTrafficRepository.GetTrafficAsync(query.LotId, query.Filter, token);
        }
    }
}
