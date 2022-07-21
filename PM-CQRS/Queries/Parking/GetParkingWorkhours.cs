using PM_Common.DTO.Parking.Workhours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingWorkhours : IQuery<List<WorkhoursDTO>>
    {
        public Int64 ParkingLotId { get; set; }
        public Int64 WorkHoursPlanId { get; set; }
    }
}
