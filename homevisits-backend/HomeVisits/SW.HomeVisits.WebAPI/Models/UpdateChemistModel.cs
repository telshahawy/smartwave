using System;
using System.Collections.Generic;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateChemistModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalPhoto { get; set; }
        public bool ExpertChemist { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinDate { get; set; }
        public List<Guid> GeoZoneIds { get; set; }
    }
}
