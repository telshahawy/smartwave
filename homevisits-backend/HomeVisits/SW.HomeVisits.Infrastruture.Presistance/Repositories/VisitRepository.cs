using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;
using System.Linq;
using System.Data.Entity;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class VisitRepository : EfRepository<HomeVisitsDomainContext>, IVisitRepository
    {
        public VisitRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void AddVisit(Visit visit)
        {
            Context.Visits.Add(visit);
        }

        public int GetLatestVisitNO()
        {
            //var visits = Context.Visits;
            var sortedVisits = from visits in Context.Visits
                               orderby Convert.ToInt32(visits.VisitNo) descending
                               select visits;
            return !sortedVisits.Any() ? 0 : int.Parse(sortedVisits.FirstOrDefault().VisitNo);
        }

        public int GetLatestVisitCode()
        {
            var visits = Context.Visits;
            return !visits.Any() ? 0 : visits.OrderByDescending(u => u.VisitCode).FirstOrDefault().VisitCode;
        }

        public void AddOnHoldVisit(OnHoldVisit onHoldVisit)
        {
            Context.OnHoldVisits.Add(onHoldVisit);
        }

        public Visit GetVisitById(Guid visitId)
        {
            // return Context.Visits.Include(x => x.VisitStatuses).SingleOrDefault(v => v.VisitId == visitId);
            var visit = Context.Visits.Find(visitId);
            Context.Entry(visit).Collection(x => x.VisitStatuses).Load();
            Context.Entry(visit).Reference(x => x.PatientAddress).Load();
            return visit;
        }

        public void PresistNewVisitStatus(VisitStatus visitStatus)
        {
            Context.VisitStatus.Add(visitStatus);
        }

        public void AddNewVisitAction(VisitAction visitAction)
        {
            Context.VisitActions.Add(visitAction);
        }

        public void UpdateVisit(Visit visit)
        {
            Context.Visits.Update(visit);
        }
    }
}
