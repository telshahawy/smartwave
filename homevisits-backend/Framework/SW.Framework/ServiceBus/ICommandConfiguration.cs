using System;
using System.Collections.Concurrent;

namespace SW.Framework.ServiceBus
{
    public interface ICommandConfiguration
    {
    }

    public class BusCommandConfigurations : ConcurrentDictionary<Type, ICommandConfiguration>
    {
        public void AddCommandConfiguration<TCommand>(ICommandConfiguration configuration) where TCommand : class
        {
            this[typeof(TCommand)] = configuration;
        }
    }
}
