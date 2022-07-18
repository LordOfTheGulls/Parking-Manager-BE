using PM_Common.DTO.Chart;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Payment
{
    public class GetTotalProfitForWeekQueryHandler : IQueryHandler<GetTotalProfitForWeek, WeeklyChartDto<double>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetTotalProfitForWeekQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<WeeklyChartDto<double>> HandleAsync(GetTotalProfitForWeek query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingPaymentRepository.GetTotalProfitForWeekAsync(query.ParkingLotId, query.Date, token);
        }
    }
}