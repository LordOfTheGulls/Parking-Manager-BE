using PM_Common.DTO.Parking.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingPricingIntervals : IQuery<Dictionary<short, IEnumerable<ParkingPricingDto>>>
    {
        public Int64 ParkingLotId { get; set; }
        public Int64 ParkingPricingPlanId { get; set; }
    }
}
