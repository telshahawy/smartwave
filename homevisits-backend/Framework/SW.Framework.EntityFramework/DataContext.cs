using SW.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;

namespace SW.Framework.EntityFrameworkCore
{
    public abstract class DataContext<TContext> : DbContext where TContext : DbContext
    {
        protected DataContext(DbContextOptions<TContext> options)
            : base(options)
        {
        }

        protected DataContext(DbContextOptionsBuilder options)
            : base(options.Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public T Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
            Entry(entity).State = EntityState.Added;

            return entity;
        }

        public T Update<T>(T entity) where T : class
        {
            if (!Set<T>().Local.Any(x => Equals(x, entity)))
            {
                Set<T>().Attach(entity);
                Entry(entity).State = EntityState.Modified;
            }

            return entity;
        }
    }
}