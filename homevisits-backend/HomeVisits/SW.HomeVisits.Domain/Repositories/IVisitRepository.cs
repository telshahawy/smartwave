using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Domain.Repositories
{
    public interface IVisitRepository : IDisposableRepository
    {
        void AddVisit(Visit visit);
        void AddOnHoldVisit(OnHoldVisit onHoldVisit);
        Visit GetVisitById(Guid visitId);
        void PresistNewVisitStatus(VisitStatus visitStatus);
        int GetLatestVisitNO();
        int GetLatestVisitCode();
        void AddNewVisitAction(VisitAction visitAction);
        void UpdateVisit(Visit visit);
    }
}
