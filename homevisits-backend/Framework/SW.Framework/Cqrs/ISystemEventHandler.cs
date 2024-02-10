namespace SW.Framework.Cqrs
{
    public interface ISystemEventHandler<in TEvent> where TEvent : class
    {
        void Handle(TEvent @event);
    }
}