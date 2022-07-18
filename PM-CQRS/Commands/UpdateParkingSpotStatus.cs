using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdateParkingSpotStatus : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public long ParkingLotId { get; set; }
        public long ParkingSpotId { get; set;}
    }
}
