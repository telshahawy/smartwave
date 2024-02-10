namespace SW.Framework.Cqrs
{
    public interface ISystemEventDecorator<in TEvent> : ISystemEventHandler<TEvent> where TEvent : class
    {
    }
}