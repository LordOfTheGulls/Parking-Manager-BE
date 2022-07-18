using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries
{
    public class GetParkingSpotStatusById : IQuery<string>
    {
        public long ParkingLotId { get; set; }
        public long ParkingSpotId { get; set; }
    }
}
