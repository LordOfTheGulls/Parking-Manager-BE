using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.Parking.Pricing
{
    public class ParkingPricingDto
    {
        public Int64 ParkingPricingIntervalId { get; set; }
        public double DayOfWeek { get; set; }
        public double IntervalStart { get; set; }
        public double IntervalEnd { get; set; }
        public double Rate { get; set; }
        public double Incremental { get; set; }
        public double IncrementalRate { get; set; }
    }
}
