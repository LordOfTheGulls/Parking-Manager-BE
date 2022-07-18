using Newtonsoft.Json;
using PM_Common.Enums;
using PM_Common.Enums.Parking;
using PM_Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO
{
    public class ParkingEventPayloadBase {}

    public class SpotChangedEventPayload : ParkingEventPayloadBase
    {
        public Int64 SpotId { get; set; }

        public bool OldSpotStatus { get; set; }

        public bool NewSpotStatus { get; set; }

       /* public override string ToString()
        {
            return $"Parking Spot {SpotId} has been set to '{(SpotTaken == true ? "Taken" : "Free")}'.";
        }*/
    }

    public class ParkingEventEmitDto
    {
        public Int64 EventLotId { get; set; }
        public ParkingEventType EventType { get; set; }
        public DateTime? EventDate { get; set; }
        public ParkingEventPayloadBase? EventPayload { get; set; }
    }
}
