using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateChemistCommand
    {
        Guid UserId { get; }
        string Name { get; }
        int Gender { get; }
        string PhoneNumber { get; }
        DateTime BirthDate { get; }
        string PersonalPhoto { get; }
        bool ExpertChemist { get; }
        bool IsActive { get; }
        DateTime JoinDate { get; }
        List<Guid> GeoZoneIds { get; }
    }
}
