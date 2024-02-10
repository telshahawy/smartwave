using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetPatientByPatientIdQuery
    {
        public Guid PatientId { get; set; }
        public Guid ClientId { get; set; }

    }
}
