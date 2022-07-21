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
    public class UpdateParkingBlacklistCommandHandler : ICommandHandler<UpdateParkingBlacklist>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public UpdateParkingBlacklistCommandHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<OperationResult> HandleAsync(UpdateParkingBlacklist command, CancellationToken cancellationToken = default)
        {
            using var uow = await _uow.OpenAsync();

            await uow.ParkingLotBlacklistRepository.UpdateParkingBlacklist(command.ParkingBlacklist.BlacklistId, command.ParkingBlacklist, cancellationToken);

            await uow.CommitAsync();

            return await Task.FromResult(new OperationResult(OperationResultStatus.Success, "Update"));
        }
    }
}
