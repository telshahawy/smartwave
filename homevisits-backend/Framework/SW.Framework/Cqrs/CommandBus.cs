using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SW.Framework.Cqrs;

namespace SW.Framework.Cqrs
{
    public sealed class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private void Send<TCommand>(TCommand command) where TCommand : class
        {
            if (command == null) throw new Exception("Command can't be null");
            var commandHandler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            commandHandler.Handle(command);
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class
        {
            await new TaskFactory().StartNew(() => Send(command));
        }
    }
}