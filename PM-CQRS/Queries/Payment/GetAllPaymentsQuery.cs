using PM_Common.DTO.Filtering;
using PM_Common.DTO.Paging;
using PM_Common.DTO.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Payment
{
    public class GetAllPaymentsQuery: IQuery<PagingResult<ParkingLotPaymentDto>>
    {
        public Int64 ParkingLotId { get; set; }
        public FilterDto Filter { get; set; }
    }
}
