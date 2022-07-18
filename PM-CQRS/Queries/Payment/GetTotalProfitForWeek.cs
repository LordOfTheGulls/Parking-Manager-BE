using PM_Common.DTO.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Payment
{
    public class GetTotalProfitForWeek : IQuery<WeeklyChartDto<double>>
    {
        public Int64 ParkingLotId { get; set; }
        public DateTime Date { get; set; }
    }
}
