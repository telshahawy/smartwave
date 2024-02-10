using System;
using System.Collections.Generic;
using System.Text;

namespace SW.Framework.Domain
{
    public interface IAggregateRoot<T>
    {
        T Id { get; }
    }
}
