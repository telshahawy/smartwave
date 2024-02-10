using System;
using System.Collections.Generic;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateRoleModel
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public bool IsActive {get;set;}
        public int DefaultPageId {get;set;}
        public List<int> Permissions {get;set;}
        public List<Guid> GeoZones {get;set;}
    }
}
