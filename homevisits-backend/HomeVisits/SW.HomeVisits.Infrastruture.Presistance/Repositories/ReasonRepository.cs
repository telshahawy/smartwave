using System;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class ReasonRepository : EfRepository<HomeVisitsDomainContext>, IReasonRepository
    {
        public ReasonRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void DeleteReason(int reasonId)
        {
            var reason = Context.Reasons.Find(reasonId);
            if (reason == null)
            {
                throw new Exception("Reason not found");
            }
            reason.IsDeleted = true;
            Context.Reasons.Update(reason);
        }

        public Reason GetReasonById(int reasonId)
        {
            return Context.Reasons.SingleOrDefault(r => r.ReasonId == reasonId);
        }

        public void PresistNewReason(Reason reason)
        {
            Context.Reasons.Add(reason);
        }

        public void UpdateReason(Reason reason)
        {
            Context.Reasons.Update(reason);
        }
    }
}
