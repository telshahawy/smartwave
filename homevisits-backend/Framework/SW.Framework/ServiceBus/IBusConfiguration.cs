using System;
using System.Collections.Concurrent;

namespace SW.Framework.ServiceBus
{
    public interface IBusConfiguration
    {
    }

    public class BusConfigurations : ConcurrentDictionary<Type, IBusConfiguration>
    {
        public void AddBusConfiguration<TCommand>(IBusConfiguration configuration) where TCommand : class
        {
            this[typeof(TCommand)] = configuration;
        }
    }
}
