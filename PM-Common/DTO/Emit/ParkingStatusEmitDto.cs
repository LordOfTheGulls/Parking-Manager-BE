using PM_Common.Enums;
using PM_Common.Enums.Parking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO
{
    public class ParkingSlotStatus
    {
        public int SpotId { get; set; }
        public bool SpotTaken { get; set; }
        public bool SpotActive { get; set; }
        public ParkingSpotType SpotType { get; set; }
    }

    public class ParkingBarrierStatus
    {
        public bool IsEntranceOpen { get; set; }

        public bool isExitOpen { get; set; }
    }

    public class ParkingStatusEmitDto
    {
        public bool? IsParkingOpen { get; set; }

        public Dictionary<int, ParkingSlotStatus>? SlotsStatuses { get; set; }

        public ParkingBarrierStatus? BarriersStatuses { get; set; }
    }
}
