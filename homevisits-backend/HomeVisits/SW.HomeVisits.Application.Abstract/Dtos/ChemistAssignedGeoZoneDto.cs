using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistAssignedGeoZoneDto
    {
        public Guid ChemistId { get; set; }
        public Guid GeoZoneId { get; set; }
        public Guid GovernateId { get; set; }
        public Guid CountryId { get; set; }
        public bool IsActive { get; set; }
        public bool GovernateIsActive { get; set; }
        public bool CountryIsActive { get; set; }
    }
}
