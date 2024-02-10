using System;

namespace SW.Framework.Domain
{
    public interface IDisposableRepository : IRepository, IDisposable
    {
    }
}