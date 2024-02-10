using System;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateChemistPermitModel
    {
        public Guid ChemistId {get;set;}
        public DateTime PermitDate {get;set;}
        public string StartTime {get;set;}
        public string EndTime {get;set;}
    }
}
