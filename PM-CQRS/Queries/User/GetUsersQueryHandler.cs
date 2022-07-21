using Microsoft.Extensions.Logging;
using PM_Common.DTO;
using PM_Common.DTO.Paging;
using PM_Common.DTO.User;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Queries.User
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, PagingResult<UserDTO>>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(ILogger<GetUsersQueryHandler> logger, IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PagingResult<UserDTO>> HandleAsync(GetUsersQuery query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.UserRepository.GetAllUsers(query.Filter, token);
        }
    }
}
