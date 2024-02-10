using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class GetAvailableVisitsInAreaModel
    {
        public Guid GeoZoneId { get; set; }
        public DateTime Date { get; set; }
    }
}
