using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.Enums.Parking
{
    public enum ParkingEventType
    {
        None                     = 0,
        Syste_Live               = 1,
        Entrance_Attempt         = 2,
        Entrance_Attempt_Blocked = 3,
        Echo_Slot_Status         = 4,
        Parking_Open             = 5,
        Parking_Closed           = 6,
        Entrance_Barrier_Opened  = 7,
        Entrance_Barrier_Closed  = 8,
        Exit_Barrier_Opened      = 9,
        Exit_Barrier_Closed      = 10,
        Parking_Slot_Taken       = 11,
        Parking_Slot_Freed       = 12,
        Lights_On                = 13,
        Lights_Off               = 14,
        Parking_Full             = 15,
        Vehicle_Entered          = 16,
        Vehicle_Exited           = 17,
        System_Paired            = 18,
        System_Pair_Attempt      = 19,
        System_Off               = 20,
    }
}
