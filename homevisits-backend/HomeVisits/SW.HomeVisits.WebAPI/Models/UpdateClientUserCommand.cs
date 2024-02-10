using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateClientUserCommand : IUpdateClientUserCommand
    {
        public Guid UserId {get;set;}

        public string Name {get;set;}

        public string PhoneNumber {get;set;}

        public bool IsActive {get;set;}

        public Guid RoleId {get;set;}

        public List<Guid> GeoZonesIds { get; set; }

        public List<int> Permissions { get; set; }
    }
}
