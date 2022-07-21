using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Parking.Pricing
{
    public class AddParkingPricingIntervalDto
    {
        public Int64 ParkingPricingPlanId { get; set; }
        public short DayOfWeek { get; set; }
    }
}
