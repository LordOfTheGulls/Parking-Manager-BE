using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Parking.Event;
using PM_Common.DTO.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries
{
    public class GetParkingEventsByLotId : IQuery<PagingResult<ParkingEventDTO>>
    {
        public Int64 LotId { get; set; }
        public FilterDto Filter { get; set; }
    }
}
