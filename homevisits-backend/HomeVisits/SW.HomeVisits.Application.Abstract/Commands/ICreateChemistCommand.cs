using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateChemistCommand
    {
        Guid UserId { get; }
        string Name { get;}
        string UserName { get;}
        string Password { get; }
        int Gender { get; }
        string PhoneNumber { get;}
        DateTime BirthDate { get; }
        string PersonalPhoto { get; }
        bool ExpertChemist { get;}
        bool IsActive { get;}
        Guid ClientId { get;}
        DateTime JoinDate { get; }
        int DOB { get; }
        Guid CreatedBy { get; }
        List<Guid> GeoZoneIds { get; }
    }
}
 