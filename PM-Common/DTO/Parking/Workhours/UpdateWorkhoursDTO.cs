using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Parking.Workhours
{
    public class UpdateWorkhoursDTO
    {
        public Int64 ParkingLotId { get; set; }
        public Int64 ParkingWorkhourPlanId { get; set; }
        public Int64 WorkhourId { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
