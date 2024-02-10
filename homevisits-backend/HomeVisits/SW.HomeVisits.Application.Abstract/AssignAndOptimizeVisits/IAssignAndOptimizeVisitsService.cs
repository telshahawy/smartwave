using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SW.HomeVisits.Application.Abstract.AssignAndOptimizeVisits
{
    public interface IAssignAndOptimizeVisitsService
    {
        Task AssignAndOptimizeVisitsInRecurringJobAsync();
        Task AssignAndOptimizeVisitsInAddVisitsAsync(VisitDetailsDto addedVisit, Guid timeZoneGeoZoneId, DateTime visitDate, Guid clientId);
    }
}
