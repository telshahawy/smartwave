using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetPatientAllVisitNotificationsQuery
    {
        public Guid PatientId { get; set; }
        CultureNames CultureName { get; }
    }
}
