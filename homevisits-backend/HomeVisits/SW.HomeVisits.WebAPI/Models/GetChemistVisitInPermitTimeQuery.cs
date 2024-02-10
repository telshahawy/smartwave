using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistVisitInPermitTimeQuery : IGetChemistVisitInPermitTimeQuery
    {
        public Guid ChemistId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime PermitDate { get; set; }
        public TimeSpan PermitStartTime { get; set; }
        public TimeSpan PermitEndTime { get; set; }
    }
}
