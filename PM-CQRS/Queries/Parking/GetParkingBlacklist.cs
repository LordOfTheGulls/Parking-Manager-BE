using PM_Common.DTO.Parking.Blacklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingBlacklist : IQuery<List<ParkingBlacklistDto>>
    {
        public Int64 ParkingLotId { get; set; }
    }
}
