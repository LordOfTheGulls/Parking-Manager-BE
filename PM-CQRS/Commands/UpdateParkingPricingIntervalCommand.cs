using PM_Common.DTO.Parking.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdateParkingPricingIntervalCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public ParkingPricingDto PricingData { get; set; }
    }
}
