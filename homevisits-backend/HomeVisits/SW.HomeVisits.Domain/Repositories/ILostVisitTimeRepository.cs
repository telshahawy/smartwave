using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface ILostVisitTimeRepository : IDisposableRepository
    {
        void PresistNewLostVisitTime(LostVisitTime lostVisitTime);

    }
}
