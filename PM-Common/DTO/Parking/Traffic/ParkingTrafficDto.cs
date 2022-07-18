using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Parking.Traffic
{
    public class ParkingTrafficDTO
    {
        public Int64 TrafficLogId { get; set; }
        public DateTime InDate { get; set; }
        public DateTime? OutDate { get; set; }
        public string LicensePlate { get; set; }
    }
}
