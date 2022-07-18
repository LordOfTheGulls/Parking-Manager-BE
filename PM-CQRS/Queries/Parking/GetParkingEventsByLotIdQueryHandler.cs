

using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Event;
using PM_DAL;
using PM_DAL.UOW;

namespace PM_CQRS.Queries
{
    public class GetParkingEventsByLotIdQueryHandler : IQueryHandler<GetParkingEventsByLotId, PagingResult<ParkingEventDTO>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingEventsByLotIdQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<PagingResult<ParkingEventDTO>> HandleAsync(GetParkingEventsByLotId query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingEventLogRepository.GetEventsAsync(query.LotId, query.Filter, token);
        }
    }
}
