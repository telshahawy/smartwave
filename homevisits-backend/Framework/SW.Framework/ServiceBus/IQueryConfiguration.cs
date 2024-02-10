using System;
using System.Collections.Concurrent;

namespace SW.Framework.ServiceBus
{
    public interface IQueryConfiguration
    {

    }

    public class BusQueryConfigurations : ConcurrentDictionary<Type, IQueryConfiguration>
    {
        public void AddQueryConfiguration<TQuery>(IQueryConfiguration configuration) where TQuery : class
        {
            this[typeof(TQuery)] = configuration;
        }
    }
}
