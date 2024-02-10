using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateRoleCommand : IUpdateRoleCommand
    {
        public Guid RoleId {get;set;}
        public Guid Client {get;set;}
        public string NameAr {get;set;}
        public string NameEn {get;set;}
        public string Description {get;set;}
        public bool IsActive {get;set;}
        public int DefaultPageId {get;set;}
        public List<int> Permissions {get;set;}
        public List<Guid> GeoZones {get;set;}
    }
}
