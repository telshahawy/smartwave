using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SearchChemistPermitDto
    {
        public Guid ChemistPermitId { get; set; }
        public DateTime PermitDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
