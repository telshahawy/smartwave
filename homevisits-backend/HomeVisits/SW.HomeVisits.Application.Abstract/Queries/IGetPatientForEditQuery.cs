using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetPatientForEditQuery
    {
        public Guid PatientId { get; set; }
    }
}
