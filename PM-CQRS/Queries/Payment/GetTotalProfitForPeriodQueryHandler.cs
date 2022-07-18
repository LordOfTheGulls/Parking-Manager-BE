using PM_Common.DTO.Parking;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Dashboard
{
    public class GetTotalProfitForPeriodQueryHandler : IQueryHandler<GetTotalProfitForPeriod, double>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetTotalProfitForPeriodQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<double> HandleAsync(GetTotalProfitForPeriod query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingPaymentRepository.GetTotalProfitForPeriod(query.ParkingLotId, query.FromDate, query.ToDate, token);
        }
    }
}
