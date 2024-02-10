using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SW.HomeVisits.Infrastruture.Data;
using SW.HomeVisits.Infrastruture.Presistance.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Presistance.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HomeVisitsDomainContext>())
                {
                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HomeVisitsDomainContext>())
                {
                    SeedExtentions.Seed(context);
                }
            }
        }
    }
}
