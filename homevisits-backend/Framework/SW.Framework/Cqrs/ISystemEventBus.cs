using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace SW.Framework.Cqrs
{
    public interface ISystemEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class;
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
    }

    internal class SystemEventBus : ISystemEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        public SystemEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var eventHandler = _serviceProvider.GetRequiredService<ISystemEventHandler<TEvent>>();
            eventHandler.Handle(@event);

        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
        {
            await new TaskFactory().StartNew(() => Publish(@event));
            //return Task.FromResult(0);
        }
    }
}