using System.Threading.Tasks;

namespace SW.Framework.Domain
{
    public interface IDomainEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class;
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
    }
}