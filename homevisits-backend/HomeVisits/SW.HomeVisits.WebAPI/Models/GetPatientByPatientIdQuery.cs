using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetPatientByPatientIdQuery : IGetPatientByPatientIdQuery
    {
        public Guid PatientId { get; set; }
        public Guid ClientId { get; set; }
    }
}
