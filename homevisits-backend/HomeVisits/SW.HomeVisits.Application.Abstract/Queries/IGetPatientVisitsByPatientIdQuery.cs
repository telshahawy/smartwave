using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetPatientVisitsByPatientIdQuery
    {
        public Guid PatientId { get; set; }
        CultureNames CultureName { get; }
    }
}
