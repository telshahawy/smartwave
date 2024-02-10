using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistAssignedGeoZoneKeyValueDto
    {
        public Guid Id { get; set; }
        public Guid GeoZoneId { get; set; }
        public string Name { get; set; }
    }
}
