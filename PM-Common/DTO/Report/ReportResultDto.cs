using PM_Common.DTO.Parking.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Report
{

    public class ReportResultDto
    {
        public ReportParkingEvent[]? ReportParkingEvent { get; set; }
    }

    public class ReportParkingEvent: ParkingEventDTO
    {

    }
}
