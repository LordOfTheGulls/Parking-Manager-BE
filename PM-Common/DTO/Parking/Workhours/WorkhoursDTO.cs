using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Parking.Workhours
{
    public class WorkhoursDTO
    {
        public Int64 WorkhourId { get; set; }
        public int WeekDay { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
