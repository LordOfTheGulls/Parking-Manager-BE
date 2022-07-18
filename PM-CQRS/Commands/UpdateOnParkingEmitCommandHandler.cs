using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Commands
{
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
        public class UpdateOnParkingEmitCommandHandler : ICommandHandler<UpdateOnParkingEmitCommand>
        {
            private readonly IDbContext<IUnitOfWork> _uow;

            public UpdateOnParkingEmitCommandHandler(IDbContext<IUnitOfWork> uow)
            {
                _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            }

            public async Task<OperationResult> HandleAsync(UpdateOnParkingEmitCommand command, CancellationToken cancellationToken = default)
            {
                using var uow = await _uow.OpenAsync();

                if (command.ParkingEvent != null)
                {
                    await uow.ParkingEventLogRepository.LogEventAsync(command.ParkingEvent, cancellationToken);
                }

                if (command.ParkingLocation != null)
                {
                    await uow.ParkingLotRepository.UpdateLocation(
                        command.ParkingLocation.ParkingLotId,
                        command.ParkingLocation.ParkingLocation.Lattitude,
                        command.ParkingLocation.ParkingLocation.Longitude,
                    cancellationToken);
                }

                await uow.CommitAsync();

                return await Task.FromResult(new OperationResult(OperationResultStatus.Success, "Update"));
            }
        }
    }

}
