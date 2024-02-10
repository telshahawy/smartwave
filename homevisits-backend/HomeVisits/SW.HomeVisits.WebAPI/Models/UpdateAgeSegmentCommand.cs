using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateAgeSegmentCommand : IUpdateAgeSegmentCommand
    {
        public Guid AgeSegmentId { get; set; }
        public string Name { get; set; }
        public int AgeFromDay { get; set; }
        public int AgeFromMonth { get; set; }
        public int AgeFromYear { get; set; }
        public int AgeToDay { get; set; }
        public int AgeToMonth { get; set; }
        public int AgeToYear { get; set; }
        public bool AgeFromInclusive { get; set; }
        public bool AgeToInclusive { get; set; }
        public bool IsActive { get; set; }
        public bool NeedExpert { get; set; }
    }
}
