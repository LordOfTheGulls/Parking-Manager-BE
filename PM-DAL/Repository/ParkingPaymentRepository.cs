using Microsoft.EntityFrameworkCore;
using PM_Common.DTO.Chart;
using PM_Common.DTO.Payment;
using PM_Common.Exceptions;
using PM_Common.Extensions.Datetime;
using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Repository
{
    public class ParkingPaymentRepository : RepositoryBase<ParkingPayment>, IParkingPaymentRepository
    {
        private readonly PMDBContext context;

        public ParkingPaymentRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task PayForStay(Int64 parkingLotId, string licensePlate, CreditCardDto creditCardInfo, CancellationToken cancellationToken = default)
        {
            var parkingInOutLog = await context.ParkingInOutLog.FirstOrDefaultAsync(e => e.ParkingLotId == parkingLotId && e.OutDateTime == null && e.LicensePlate == licensePlate, cancellationToken);

            if (parkingInOutLog == null)
            {
                throw new EntityDoesNotExistException(0, typeof(ParkingInOutLog));
            }

            double hourlyRate = 2;

            DateTime entranceTime = parkingInOutLog.InDateTime;

            DateTime exitTime = DateTime.UtcNow;

            TimeSpan stayTime = exitTime.Subtract(entranceTime);

            await context.ParkingPayment.AddAsync(new ParkingPayment()
            {
               ParkingLotId      = parkingLotId,
               ParkingInOutLogId = parkingInOutLog.Id,
               Amount            = stayTime.Hours * hourlyRate,
            }, cancellationToken);
        }


        public async Task<double> GetTotalProfitForPeriod(Int64 parkingLotId, DateTime fromDate, DateTime? toDate, CancellationToken cancellationToken = default)
        {
            if(toDate == null) toDate = fromDate;

            var query = context.ParkingPayment
                               .AsNoTracking()
                               .Where(e => e.ParkingLotId == parkingLotId && (e.DateCreated.Date >= fromDate.Date && e.DateCreated.Date <= toDate.GetValueOrDefault().Date));

            var query2 = query.ToListAsync(cancellationToken);

            double totalProfit = await query.SumAsync(e => e.Amount, cancellationToken);

            return totalProfit;
        }

        public async Task<WeeklyChartDto<double>> GetTotalProfitForWeekAsync(Int64 parkingLotId, DateTime date, CancellationToken cancellationToken = default)
        {
            var result = new WeeklyChartDto<double>();

            DateTime startOfWeekDate = date.StartOfWeek(DayOfWeek.Monday);

            DateTime endOfWeekDate = date.AddDays(6);

            var query = context.ParkingPayment
                               .AsNoTracking()
                               .Where(t => t.ParkingLotId == parkingLotId && t.DateCreated.Date >= startOfWeekDate.Date && t.DateCreated.Date <= endOfWeekDate.Date)
                               .OrderBy(t => t.DateCreated.Date)
                               .GroupBy(t => t.DateCreated.Date)
                               .Select(t => new { Key = t.Key, Value = t.Sum(t => t.Amount) });

            result.WeeklyData = await query.ToDictionaryAsync(x => ((int)x.Key.DayOfWeek + 6) % 7, x => x.Value, cancellationToken);

            return result;
        }
    }
}
