using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateAgeSegmentCommand
    {
        Guid AgeSegmentId { get; set; }
        string Name { get; set; }
        int AgeFromDay { get; set; }
        int AgeFromMonth { get; set; }
        int AgeFromYear { get; set; }
        int AgeToDay { get; set; }
        int AgeToMonth { get; set; }
        int AgeToYear { get; set; }
        bool AgeFromInclusive { get; set; }
        bool AgeToInclusive { get; set; }
        bool IsActive { get; set; }
        bool NeedExpert { get; set; }
    }
}
