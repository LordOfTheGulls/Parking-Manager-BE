using PM_CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_CQRS.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
           _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var service = _serviceProvider.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;

            await service.HandleAsync(command, new CancellationToken());
        }
    }
}