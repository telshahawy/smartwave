using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SW.HomeVisits.Application.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Data
{
    public class HomeVisitsDomainContextFactory : IDesignTimeDbContextFactory<HomeVisitsDomainContext>
    {
        private static string DataConnectionString => new DatabaseConfiguration().GetDataConnectionString();

        public HomeVisitsDomainContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeVisitsDomainContext>();

            optionsBuilder.UseSqlServer(DataConnectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(HomeVisitsDomainContext).GetTypeInfo().Assembly.FullName);
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            return new HomeVisitsDomainContext(optionsBuilder.Options);
        }
    }
}
