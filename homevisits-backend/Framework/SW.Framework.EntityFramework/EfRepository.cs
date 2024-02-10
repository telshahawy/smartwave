using Microsoft.EntityFrameworkCore;
using SW.Framework;
using SW.Framework.Domain;
using SW.Framework.EntityFrameworkCore;

namespace SW.Framework.EntityFrameworkCore
{
    public abstract class EfRepository<TContext> : DisposableObject, IDisposableRepository
        where TContext : DbContext //DataContext<TContext>
    {
        protected EfRepository(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }

        protected override void Disposing()
        {
            Context?.Dispose();
        }
    }
}