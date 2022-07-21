using PM_Common.DTO;
using PM_Common.Enums;
using PM_DAL;
using PM_DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
    public class UpdatePermissionCommandHandler : ICommandHandler<UpdatePermissionCommand>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public UpdatePermissionCommandHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<OperationResult> HandleAsync(UpdatePermissionCommand command, CancellationToken cancellationToken)
        {
            using var uow = await _uow.OpenAsync();

            await uow.UserRepository.UpdatePermission(command.UserId, command.UserRights, cancellationToken);

            await uow.CommitAsync();

            return null;
            //return Task.FromResult(new OperationResult(OperationResultStatus.Success, "Update"));
        }
    }
}