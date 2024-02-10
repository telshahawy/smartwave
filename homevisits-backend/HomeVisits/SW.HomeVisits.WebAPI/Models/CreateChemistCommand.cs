using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateChemistCommand : ICreateChemistCommand
    {
        public Guid UserId {get;set;}
        public string UserName {get;set;}
        public string Name { get; set; }
        public string Password {get;set;}

        public int Gender {get;set;}

        public string PhoneNumber {get;set;}

        public DateTime BirthDate {get;set;}

        public string PersonalPhoto {get;set;}

        public bool ExpertChemist {get;set;}

        public bool IsActive {get;set;}

        public Guid ClientId {get;set;}

        public DateTime JoinDate {get;set;}

        public int DOB {get;set;}

        public Guid CreatedBy {get;set;}

        public List<Guid> GeoZoneIds { get; set; }
    }
}
