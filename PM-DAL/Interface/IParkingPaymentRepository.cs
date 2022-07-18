using PM_Common.DTO.Chart;
using PM_Common.DTO.Payment;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IParkingPaymentRepository : IRepositoryBase<ParkingPayment>, IRepository
    {
        public Task PayForStay(Int64 parkingLotId, string licensePlate, CreditCardDto creditCardInfo, CancellationToken cancellationToken = default);
        public Task<double> GetTotalProfitForPeriod(Int64 parkingLotId, DateTime fromDate, DateTime? toDate, CancellationToken cancellationToken);
        public Task<WeeklyChartDto<double>> GetTotalProfitForWeekAsync(Int64 parkingLotId, DateTime date, CancellationToken cancellationToken);
    }
}
