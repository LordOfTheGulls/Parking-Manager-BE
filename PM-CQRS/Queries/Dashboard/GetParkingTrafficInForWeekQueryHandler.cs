using PM_Common.DTO.Chart;
using PM_Common.DTO.Parking.Traffic;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Dashboard
{
    public class GetParkingTrafficInForWeekQueryHandler : IQueryHandler<GetParkingTrafficInForWeek, WeeklyChartDto<int>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetParkingTrafficInForWeekQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<WeeklyChartDto<int>> HandleAsync(GetParkingTrafficInForWeek query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingTrafficRepository.GetTrafficInForWeekAsync(query.ParkingLotId, query.Date, token);
        }
    }
}
