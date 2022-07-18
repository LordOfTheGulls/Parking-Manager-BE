using PM_Common.DTO;
using PM_Common.Enums;
using PM_Common.Enums.Parking;
using PM_DAL;
using PM_DAL.Entity;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries
{
    public class GetParkingSpotStatusByIdHandler : IQueryHandler<GetParkingSpotStatusById, string>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingSpotStatusByIdHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<string> HandleAsync(GetParkingSpotStatusById query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            await uow.ParkingEventLogRepository.LogEventAsync(new ParkingEventEmitDto()
            {
                EventLotId = 1,
                EventType = (ParkingEventType)1
            });

            await uow.CommitAsync();

            return Task.FromResult("Test").Result;
        }
    }
}
