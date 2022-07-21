using PM_Common.DTO.Parking;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingLotInfoQueryHandler : IQueryHandler<GetParkingLotInfoQuery, ParkingLotDto>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingLotInfoQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<ParkingLotDto> HandleAsync(GetParkingLotInfoQuery query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingLotRepository.GetParkingLotInfo(query.ParkingLotId, token);
        }
    }
}
