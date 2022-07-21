using PM_Common.DTO.Paging;
using PM_Common.DTO.Payment;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.Payment
{
    public class GetAllPaymentsQueryHandler : IQueryHandler<GetAllPaymentsQuery, PagingResult<ParkingLotPaymentDto>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public GetAllPaymentsQueryHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<PagingResult<ParkingLotPaymentDto>> HandleAsync(GetAllPaymentsQuery query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.ParkingPaymentRepository.GetAllPayments(query.ParkingLotId, query.Filter, token);
        }
    }
}

