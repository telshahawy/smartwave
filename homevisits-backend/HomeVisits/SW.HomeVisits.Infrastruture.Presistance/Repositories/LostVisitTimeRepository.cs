using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class LostVisitTimeRepository : EfRepository<HomeVisitsDomainContext>, ILostVisitTimeRepository
    {
        public LostVisitTimeRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void PresistNewLostVisitTime(LostVisitTime lostVisitTime)
        {
            Context.LostVisitTimes.Add(lostVisitTime);
        }
    }
}
