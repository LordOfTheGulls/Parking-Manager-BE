using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetAverageStayTimeForPeriodQueryHandler : IQueryHandler<GetAverageStayTimeForPeriod, double>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetAverageStayTimeForPeriodQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<double> HandleAsync(GetAverageStayTimeForPeriod query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingTrafficRepository.GetAverageStayForPeriodAsync(query.ParkingLotId, query.FromDate, query.ToDate, token);
        }
    }
}
