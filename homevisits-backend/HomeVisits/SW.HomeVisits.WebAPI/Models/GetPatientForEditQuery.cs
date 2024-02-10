using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetPatientForEditQuery : IGetPatientForEditQuery
    {
        public Guid PatientId { get; set; }
    }
}
