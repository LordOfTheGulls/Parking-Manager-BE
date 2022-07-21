using Microsoft.Extensions.Logging;
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
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDTO>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<GetUserQueryHandler> _logger;

        public GetUserQueryHandler(ILogger<GetUserQueryHandler> logger, IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserDTO> HandleAsync(GetUserQuery query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.UserRepository.GetUser(query.UserId, token);
        }
    }
}
