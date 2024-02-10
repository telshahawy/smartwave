using System;

namespace SW.Framework.Domain
{
    public interface IDisposableUnitOfWork : IUnitOfWork, IDisposable
    {
    }
}