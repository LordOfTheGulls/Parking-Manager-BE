using PM_Common.DTO.Chart;
using PM_Common.DTO.Parking.Traffic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Dashboard
{
    public class GetParkingTrafficInForWeek : IQuery<WeeklyChartDto<int>>
    {
        public Int64 ParkingLotId { get; set; }
        public DateTime Date { get; set; }
    }
}
