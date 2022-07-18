using Microsoft.EntityFrameworkCore;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Traffic;
using PM_Common.DTO.Sorting;
using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM_Common.Extensions.Datetime;
using PM_Common.DTO.Chart;

namespace PM_DAL.Repository
{
    public class ParkingTrafficRepository : RepositoryBase<ParkingInOutLog>, IParkingTrafficRepository
    {
        private readonly PMDBContext context;

        public ParkingTrafficRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetTotalTrafficInByPeriodAsync(Int64 parkingLotId, DateTime fromDate, DateTime? toDate, CancellationToken cancellationToken)
        {
            if(toDate == null) toDate = fromDate;

            return await context.ParkingInOutLog.AsNoTracking()
                                .CountAsync(t => t.ParkingLotId == parkingLotId && (t.InDateTime.Date >= fromDate.Date && t.InDateTime.Date <= toDate.GetValueOrDefault().Date), cancellationToken);
        }

        public async Task<WeeklyChartDto<int>> GetTrafficInForWeekAsync(Int64 parkingLotId, DateTime date, CancellationToken cancellationToken = default)
        {
            var result = new WeeklyChartDto<int>();

            DateTime startOfWeekDate = date.StartOfWeek(DayOfWeek.Monday);

            DateTime endOfWeekDate   = date.AddDays(6);

            var query = context.ParkingInOutLog
                               .AsNoTracking()
                               .Where(t => t.ParkingLotId == parkingLotId && t.InDateTime.Date >= startOfWeekDate.Date && t.InDateTime.Date <= endOfWeekDate.Date)
                               .OrderBy(t => t.InDateTime.Date)
                               .GroupBy(t => t.InDateTime.Date)
                               .Select(t => new { Key = t.Key, Value = t.Count() });

            result.WeeklyData = await query.ToDictionaryAsync(x => ((int)x.Key.DayOfWeek + 6) % 7, x => x.Value, cancellationToken);

            return result;
        }

        public async Task<PagingResult<ParkingTrafficDTO>> GetTrafficAsync(Int64 parkingLotId, FilterDto filter, CancellationToken cancellationToken = default)
        {
            var result = new PagingResult<ParkingTrafficDTO>();

            result.TotalRecords = await context.ParkingInOutLog.CountAsync(e => e.ParkingLotId == parkingLotId, cancellationToken);

            var query = context.ParkingInOutLog.AsNoTracking().Where(e => e.ParkingLotId == parkingLotId);

            if (filter.Sorting != null)
            {
               /* foreach (SortingDto sort in filter.Sorting)
                {
                    if (sort.SortOrder != SortOrder.None)
                    {
                        switch (sort.ColumnId)
                        {
                            case "eventDate":
                                {
                                    query = (sort.SortOrder == SortOrder.Ascending) ?
                                        query.OrderBy(v => v.EventDate) :
                                        query.OrderByDescending(v => v.EventDate);
                                    break;
                                }
                            default: continue;
                        }
                    }
                }*/
            }
            else
            {
                query = query.OrderByDescending(v => v.InDateTime);
            }

            result.Records = await query
            .Skip(filter.Paging.Skip)
            .Take(filter.Paging.Take)
            .Select(e => new ParkingTrafficDTO()
            {
                TrafficLogId = e.Id,
                LicensePlate = e.LicensePlate,
                InDate       = e.InDateTime,
                OutDate      = e.OutDateTime,
            }).ToListAsync(cancellationToken);

            return result;
        }

        public async Task<double> GetAverageStayForPeriodAsync(Int64 parkingLotId, DateTime fromDate, DateTime? toDate, CancellationToken cancellationToken = default)
        {
            if (toDate == null) toDate = fromDate;

            var query = context.ParkingInOutLog
                               .AsNoTracking()
                               .Where(e => e.ParkingLotId == parkingLotId && (e.InDateTime.Date >= fromDate.Date && e.InDateTime.Date <= toDate.GetValueOrDefault().Date) && e.OutDateTime != null);

            var result = await query.ToListAsync(cancellationToken);

            double total = 0;

            foreach(var item in result)
            {
                total += item.OutDateTime.GetValueOrDefault().Subtract(item.InDateTime).TotalMinutes;
            }

            return Math.Round(total / (double)result.Count(), 2);
        }
    }
}
