using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Dashboard
{
    public class GetTotalParkingTrafficInForPeriodQueryHandler : IQueryHandler<GetTotalParkingTrafficInForPeriod, int>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetTotalParkingTrafficInForPeriodQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<int> HandleAsync(GetTotalParkingTrafficInForPeriod query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingTrafficRepository.GetTotalTrafficInByPeriodAsync(query.ParkingLotId, query.FromDate, query.ToDate, token);
        }
    }
}
