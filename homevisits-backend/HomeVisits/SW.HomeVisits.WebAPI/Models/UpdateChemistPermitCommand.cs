using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateChemistPermitCommand : IUpdateChemistPermitCommand
    {
        public Guid ChemistPermitId {get;set;}
        public DateTime PermitDate {get;set;}
        public TimeSpan StartTime {get;set;}
        public TimeSpan EndTime {get;set;}
        public Guid ClientId {get;set;}
    }
}
