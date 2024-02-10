using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SearchChemistScheduleDto
    {
        public Guid ChemistScheduleId { get; set; }
        public string GeoZoneName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
