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
    public class AddParkingPricingIntervalCommandHandler : ICommandHandler<AddParkingPricingIntervalCommand>
    {
        private readonly IDbContext<IUnitOfWork> _uow;

        public AddParkingPricingIntervalCommandHandler(IDbContext<IUnitOfWork> uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public Task<CommandValidationResult> CanHandle()
        {
            return Task.FromResult(CommandValidation.Succeeded("Good"));
        }

        public async Task<OperationResult> HandleAsync(AddParkingPricingIntervalCommand command, CancellationToken cancellationToken = default)
        {
            using var uow = await _uow.OpenAsync();

            await uow.ParkingPricingRepository.AddParkingPricingInterval(command.ParkingLotId, command.ParkingPricingPlanId, command.DayOfWeek, cancellationToken);

            await uow.CommitAsync();

            return await Task.FromResult(new OperationResult(OperationResultStatus.Success, "Update"));
        }
    }
}
