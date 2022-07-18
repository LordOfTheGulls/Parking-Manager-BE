using PM_Common.DTO;
using PM_Common.DTO.Chart;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Event;
using PM_Common.DTO.Report;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IParkingEventLogRepository : IRepositoryBase<ParkingEventLog>, IRepository
    {
        public Task<WeeklyChartDto<int>> GetTotalParkingEventsForWeekAsync(Int64 parkingLotId, DateTime date, CancellationToken cancellationToken = default);

        public IEnumerable<ReportParkingEvent> GetParkingEventReportByPeriodStreamed(ReportByPeriodFilterDto reportFilter, int chunkSize = 10, CancellationToken cancellationToken = default);

        public IEnumerable<ReportParkingEvent> GetParkingEventReportByDateStreamed(ReportByDateFilterDto reportFilter, int chunkSize = 10, CancellationToken cancellationToken = default);

        public Task<ReportResultDto> GetParkingEventReportByDate(ReportByDateFilterDto reportFilter, CancellationToken cancellationToken = default);

        public Task<PagingResult<ReportCountResultDto>> GetParkingEventReportCounts(ReportCountFilterDto reportFilter, CancellationToken cancellationToken = default);

        public Task<PagingResult<ParkingEventDTO>> GetEventsAsync(Int64 parkingLotId, FilterDto filter, CancellationToken cancellationToken = default);

        public Task LogEventAsync(ParkingEventEmitDto dto, CancellationToken cancellationToken = default);
    }
}
