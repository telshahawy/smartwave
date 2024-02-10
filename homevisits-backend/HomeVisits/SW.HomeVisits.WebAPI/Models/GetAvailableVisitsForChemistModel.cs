using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class GetAvailableVisitsForChemistModel
    {
        public Guid ChemistId { get; set; }

        public Guid? GeoZoneId { get; set; }

        public DateTime Date { get; set; }

    }
}
