using PM_Common.DTO.Chart;
using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Traffic;
using PM_DAL.Entity;
using PM_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_DAL.Interface
{
    public interface IParkingTrafficRepository : IRepositoryBase<ParkingInOutLog>, IRepository
    {
        public Task<PagingResult<ParkingTrafficDTO>> GetTrafficAsync(Int64 parkingLotId, FilterDto filter, CancellationToken cancellationToken = default);
        public Task<int> GetTotalTrafficInByPeriodAsync(Int64 parkingLotId, DateTime fromDate, DateTime? toDate, CancellationToken cancellationToken = default);
        public Task<WeeklyChartDto<int>> GetTrafficInForWeekAsync(Int64 parkingLotId, DateTime date, CancellationToken cancellationToken = default);
        public Task<double> GetAverageStayForPeriodAsync(Int64 parkingLotId, DateTime fromDate, DateTime? toDate, CancellationToken cancellationToken = default);
    }
}
