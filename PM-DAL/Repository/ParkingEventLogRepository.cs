using PM_Common.DTO;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Event;
using PM_DAL.Entity;
using PM_DAL.Interface;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PM_Common.Enums;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Sorting;
using PM_Common.Enums.Parking;
using PM_Common.DTO.Report;
using PM_Common.Enums.Report;
using PM_Common.DTO.Chart;
using PM_Common.Extensions.Datetime;

namespace PM_DAL.Repository
{
    public class ParkingEventLogRepository : RepositoryBase<ParkingEventLog>, IParkingEventLogRepository
    {
        private PMDBContext context;

        public ParkingEventLogRepository(PMDBContext dbContext) : base(dbContext)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<WeeklyChartDto<int>> GetTotalParkingEventsForWeekAsync(Int64 parkingLotId, DateTime date, CancellationToken cancellationToken = default)
        {
            var result = new WeeklyChartDto<int>();

            DateTime startOfWeekDate = date.StartOfWeek(DayOfWeek.Monday);

            DateTime endOfWeekDate = date.AddDays(6);

            var query = context.ParkingEventLog
                               .AsNoTracking()
                               .Where(t => t.ParkingLotId == parkingLotId && t.EventDate.Date >= startOfWeekDate.Date && t.EventDate.Date <= endOfWeekDate.Date)
                               .OrderBy(t => t.EventDate.Date)
                               .GroupBy(t => t.EventDate.Date)
                               .Select(t => new { Key = t.Key, Value = t.Count() });

            result.WeeklyData = await query.ToDictionaryAsync(x => ((int)x.Key.DayOfWeek + 6) % 7, x => x.Value, cancellationToken);

            return result;
        }


        public IEnumerable<ReportParkingEvent> GetParkingEventReportByPeriodStreamed(ReportByPeriodFilterDto reportFilter, int chunkSize = 10, CancellationToken cancellationToken = default)
        {
            var query = context.ParkingEventLog
                        .AsNoTracking()
                        .Where(e => e.ParkingLotId == reportFilter.ParkingLotId && (e.EventDate.Date >= reportFilter.FromDate.Date && e.EventDate.Date <= reportFilter.ToDate.Date));

            int totalEventRecords = query.Count();

            for (int i = 0; i < totalEventRecords + chunkSize; i += chunkSize)
            {
                foreach (ReportParkingEvent parkingEventReportRecord in query.Skip(i).Take(chunkSize).Select(x => new ReportParkingEvent()
                {
                    EventLogId = x.Id,
                    EventId = x.Event.Id,
                    EventName = x.Event.Name,
                    EventDate = x.EventDate,
                }).AsEnumerable())
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        yield break;
                    }

                    yield return parkingEventReportRecord;
                }
            }
        }

        public IEnumerable<ReportParkingEvent> GetParkingEventReportByDateStreamed(ReportByDateFilterDto reportFilter, int chunkSize = 10, CancellationToken cancellationToken = default)
        {
            var query = context.ParkingEventLog
                               .AsNoTracking()
                               .Where(e => e.ParkingLotId == reportFilter.ParkingLotId && e.EventDate.Date == reportFilter.ForDate);

            int totalEventRecords = query.Count();

            for (int i = 0; i < totalEventRecords + chunkSize; i += chunkSize)
            {
                foreach (ReportParkingEvent parkingEventReportRecord in query.Skip(i).Take(chunkSize).Select(x => new ReportParkingEvent()
                {
                    EventLogId = x.Id,
                    EventId    = x.Event.Id,
                    EventName  = x.Event.Name,
                    EventDate  = x.EventDate,
                }).AsEnumerable())
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        yield break;
                    }

                    yield return parkingEventReportRecord;
                }
            }
        }

        public async Task<ReportResultDto> GetParkingEventReportByDate(ReportByDateFilterDto reportFilter, CancellationToken cancellationToken = default)
        {
            var result = new ReportResultDto();

            var query = context.ParkingEventLog
                               .AsNoTracking()
                               .Where(e => e.ParkingLotId == reportFilter.ParkingLotId && e.EventDate.Date == reportFilter.ForDate.Date);

            result.ReportParkingEvent = await query.Select(e => new ReportParkingEvent()
                                        {
                                            EventDate   = e.EventDate,
                                            EventId     = e.EventId,
                                            EventLogId  = e.Id,
                                            EventName   = e.Event.Name,
                                            EventType   = (ParkingEventType)e.Event.Id
                                        }).ToArrayAsync(cancellationToken);
            return result;
        }

        public async Task<PagingResult<ReportCountResultDto>> GetParkingEventReportCounts(ReportCountFilterDto reportFilter, CancellationToken cancellationToken = default)
        {
            var pagingResult = new PagingResult<ReportCountResultDto>();

            var paging = reportFilter.Filter.Paging;

            var query = context.ParkingEventLog
                        .AsNoTracking()
                        .Where(e => e.ParkingLotId == reportFilter.ParkingLotId && (e.EventDate.Date >= reportFilter.FromDate.GetValueOrDefault().Date && e.EventDate.Date <= reportFilter.ToDate.GetValueOrDefault().Date))
                        .GroupBy(x => new { EventDate = x.EventDate.Date });

            pagingResult.TotalRecords = await query.CountAsync(cancellationToken);
                 pagingResult.Records = await query
                                      .Skip(paging.Skip)
                                      .Take(paging.Take)
                                      .Select(x => new ReportCountResultDto(){
                                        TotalRecords = x.Count(),
                                        ReportType = ReportType.ParkingEventReport,
                                        Date = x.Key.EventDate.Date,
                                      }).ToListAsync(cancellationToken);

            return pagingResult;
        }


        public async Task<PagingResult<ParkingEventDTO>> GetEventsAsync(Int64 parkingLotId, FilterDto filter, CancellationToken cancellationToken = default)
        {
            var result = new PagingResult<ParkingEventDTO>();

            result.TotalRecords = await context.ParkingEventLog.CountAsync(e => e.ParkingLotId == parkingLotId, cancellationToken);

            var query = context.ParkingEventLog.AsNoTracking().Where(e => e.ParkingLotId == parkingLotId);

            if(filter.Sorting != null)
            {
                foreach (SortingDto sort in filter.Sorting)
                {
                    if(sort.SortOrder != SortOrder.None)
                    {
                        switch (sort.ColumnId)
                        {
                            case "eventDate": {
                                    query = (sort.SortOrder == SortOrder.Ascending) ? 
                                        query.OrderBy(v => v.EventDate) : 
                                        query.OrderByDescending(v => v.EventDate);
                                    break;
                            }
                            default: continue;
                        }
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(v => v.EventDate);
            }
            
            result.Records = await query
            .Skip(filter.Paging.Skip)
            .Take(filter.Paging.Take)   
            .Select(e => new ParkingEventDTO()
            {
                EventId     = e.Event.Id,
                EventLogId  = e.Id,
                EventDate   = e.EventDate,
                EventType   = (ParkingEventType)e.Event.Id,
                EventName   = e.Event.Name,
            }).ToListAsync(cancellationToken);

            return result;
        }

        public async Task LogEventAsync(ParkingEventEmitDto dto, CancellationToken cancellationToken = default)
        {
            await context.ParkingEventLog.AddAsync(new ParkingEventLog()
            {
                ParkingLotId = dto.EventLotId,
                EventId      = (short)dto.EventType,
            });
        }
    }
}
