using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchPatientScheduleQuery: IPaggingQuery
    {
        DateTime? VisitDate { get; }
        Guid? PatientId { get; }
        int? VisitStatusTypeId { get; }
        Guid ClientId { get; }
        CultureNames cultureName { get; }
      
    }
}
