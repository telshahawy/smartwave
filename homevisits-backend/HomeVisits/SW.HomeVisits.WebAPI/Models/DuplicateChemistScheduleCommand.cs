using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DuplicateChemistScheduleCommand : IDuplicateChemistScheduleCommand
    {
        public Guid ChemistScheduleId {get;set;}

        public Guid NewChemistScheduleId {get;set;}

        public Guid ClientId {get;set;}

        public DateTime StartDate {get;set;}

        public DateTime EndDate {get;set;}

        public Guid CreatedBy {get;set;}

        public DateTime CreatedAt {get;set;}
    }
}
