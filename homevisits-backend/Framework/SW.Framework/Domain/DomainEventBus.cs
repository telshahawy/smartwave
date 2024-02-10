using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SW.Framework.Domain
{
    internal class DomainEventBus : IDomainEventBus
    {
        private static readonly Type GenericEventHandlerType = typeof(IDomainEventHandler<>);
        private readonly IServiceProvider _provider;

        public DomainEventBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            ProcessPublishing(@event);
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
        {
            await new TaskFactory().StartNew(() => ProcessPublishing(@event));
        }

        private void ProcessPublishing<TEvent>(TEvent @event) where TEvent : class
        {
            var eventType = @event.GetType();
            var handlerType = GenericEventHandlerType.MakeGenericType(eventType);

            var handlers = _provider.GetServices(handlerType).ToList();

            var handleMethod = handlerType.GetMethod("Handle");

            foreach (var handler in handlers)
                handleMethod.Invoke(handler, new object[] {@event});
        }
    }
}