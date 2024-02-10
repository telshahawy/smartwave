using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistWithAssignedGeoZonesDto
    {
        public Guid ChemistId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public Guid ClientId { get; set; }
        public DateTime Birthdate { get; set; }
        public string UserName { get; set; }
        public string PersonalPhoto { get; set; }
        public bool ExpertChemist { get; set; }
        public List<ChemistAssignedGeoZoneDto> GeoZones { get; set; }

    }
}
