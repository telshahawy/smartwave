using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class ChemistTrackingLogRepository : EfRepository<HomeVisitsDomainContext>, IChemistTrackingLogRepository
    {
        public ChemistTrackingLogRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void PresistNewChemistTrackingLog(ChemistTrackingLog chemistTrackingLog)
        {
            Context.ChemistTrackingLogs.Add(chemistTrackingLog);
        }

    }
}
