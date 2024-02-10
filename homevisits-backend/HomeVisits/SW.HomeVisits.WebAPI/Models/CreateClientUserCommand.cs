using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateClientUserCommand : ICreateClientUserCommand
    {
        public Guid UserId {get;set;}

        public string Name {get;set;}

        public string PhoneNumber {get;set;}

        public Guid RoleId {get;set;}

        public List<Guid> GeoZones {get;set;}

        public bool IsActive {get;set;}

        public string UserName {get;set;}

        public string Password {get;set;}

        public Guid ClientId {get;set;}

        public Guid CreateBy {get;set; }
        public List<int> Permissions { get; set; }
    }
}
