using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class AddParkingPricingIntervalCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 ParkingLotId { get; set; }
        public Int64 ParkingPricingPlanId { get; set; } = 1;
        public short DayOfWeek { get; set; }
    }
}
