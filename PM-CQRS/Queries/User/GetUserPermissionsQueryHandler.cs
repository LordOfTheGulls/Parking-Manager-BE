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
    public class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, UserRightDto>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        private readonly ILogger<GetUserPermissionsQueryHandler> _logger;

        public GetUserPermissionsQueryHandler(ILogger<GetUserPermissionsQueryHandler> logger, IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserRightDto> HandleAsync(GetUserPermissionsQuery query, CancellationToken token = default)
        {
            using var uow = await _uow.OpenAsync();

            return await uow.UserRepository.GetUserPermissions(query.UserId, token);
        }
    }
}
