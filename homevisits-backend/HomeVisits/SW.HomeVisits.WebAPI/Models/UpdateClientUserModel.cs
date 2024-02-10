using System;
using System.Collections.Generic;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateClientUserModel
    {
        public string Name {get;set;}

        public string PhoneNumber {get;set;}

        public bool IsActive {get;set;}

        public Guid RoleId {get;set;}

        public List<Guid> GeoZonesIds { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool SendCredentials { get; set; }
        public List<int> Permissions { get; set; }
    }
}
