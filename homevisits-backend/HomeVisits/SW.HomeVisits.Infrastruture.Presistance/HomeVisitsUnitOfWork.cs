using System;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Application;

namespace SW.HomeVisits.Infrastruture.Data
{
    internal class HomeVisitsUnitOfWork : EfUnitOfWork<HomeVisitsDomainContext>, IHomeVisitsUnitOfWork
    {
        public HomeVisitsUnitOfWork(HomeVisitsDomainContext context, IServiceProvider dependencyContainer)
            : base(context, dependencyContainer)
        {
        }
    }
}