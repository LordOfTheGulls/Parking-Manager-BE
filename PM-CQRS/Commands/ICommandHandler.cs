
using PM_Common.DTO;

namespace PM_CQRS.Commands
{
    public interface ICommandHandler<TCommand> where TCommand: ICommand
    {
        public virtual Task<CommandValidationResult> CanHandle()
        {
            return Task.FromResult(new CommandValidationResult("", true));
        }

        public Task<OperationResult> HandleAsync(TCommand command, CancellationToken token);
    }
}
