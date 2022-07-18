using PM_Common.DTO.Parking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Dashboard
{
    public class GetTotalProfitForPeriod : IQuery<double>
    {
        public Int64 ParkingLotId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
