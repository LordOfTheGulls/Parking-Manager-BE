using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Event;
using PM_Common.DTO.Parking.Traffic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Parking
{
    public class GetParkingTrafficByLotId : IQuery<PagingResult<ParkingTrafficDTO>>
    {
        public Int64 LotId { get; set; }
        public FilterDto Filter { get; set; }
    }
}
