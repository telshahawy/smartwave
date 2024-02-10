namespace SW.Framework.Domain
{
    public interface IDomainEventHandler<in TEvent> where TEvent : class
    {
        void Handle(TEvent @event);
    }
}