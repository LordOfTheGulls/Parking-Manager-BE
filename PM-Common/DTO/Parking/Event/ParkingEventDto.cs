using PM_Common.DTO.Paging;
using PM_Common.Enums;
using PM_Common.Enums.Parking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Parking.Event
{
    public class ParkingEventDTO
    {
        public Int64 EventLogId { get; set; }
        public Int64 EventId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public ParkingEventType EventType { get; set; } = ParkingEventType.None;
    }
}
