using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using SW.Framework;
using SW.Framework.Domain;

namespace SW.Framework.EntityFrameworkCore
{
    public abstract class EfUnitOfWork<TContext> : DisposableObject, IDisposableUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IServiceProvider _serviceProvider;

        protected EfUnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public TRepository Repository<TRepository>() where TRepository : class, IRepository
        {
            return _serviceProvider.GetRequiredService<TRepository>();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                var rethrow = OnSaveError(exception);
                if (rethrow)
                    throw;
            }
        }

        protected virtual bool OnSaveError(Exception exception)
        {
            return true;
        }

        protected override void Disposing()
        {
            _context?.Dispose();
        }

        void SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

      
    }
    
}