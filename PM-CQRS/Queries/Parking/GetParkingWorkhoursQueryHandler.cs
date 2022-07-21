using PM_Common.DTO.Parking.Workhours;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingWorkhoursQueryHandler : IQueryHandler<GetParkingWorkhours, List<WorkhoursDTO>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingWorkhoursQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<List<WorkhoursDTO>> HandleAsync(GetParkingWorkhours query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingWorkhoursRepository.GetWorkhours(query.ParkingLotId, query.WorkHoursPlanId, token);
        }
    }
}
