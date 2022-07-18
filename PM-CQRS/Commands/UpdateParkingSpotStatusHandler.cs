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
    public class UpdateParkingSpotStatusHandler : ICommandHandler<UpdateParkingSpotStatus>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public UpdateParkingSpotStatusHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<OperationResult> HandleAsync(UpdateParkingSpotStatus command, CancellationToken cancellationToken)
        {
            using var uow = await _uow.OpenAsync();

            await uow.ParkingEventLogRepository.LogEventAsync(null, cancellationToken);

            await uow.CommitAsync();

            return null;
            //return Task.FromResult(new OperationResult(OperationResultStatus.Success, "Update"));
        }
    }
}
