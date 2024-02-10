using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateClientUserModel
    {
        public string Name {get;set;}

        public string PhoneNumber {get;set;}

        public Guid RoleId {get;set;}

        public List<Guid> GeoZones {get;set;}

        public bool IsActive {get;set;}

        public string UserName {get;set;}

        public string Password {get;set; }
        public List<int> Permissions { get; set; }
        public bool SendCredentials { get; set; }
    }
}
