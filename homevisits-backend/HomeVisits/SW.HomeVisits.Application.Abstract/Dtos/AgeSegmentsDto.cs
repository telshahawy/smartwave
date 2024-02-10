using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class AgeSegmentsDto
    {
        public Guid AgeSegmentId { get; set; }
        public string Name { get; set; }
        public int AgeFromDay { get; set; }
        public int AgeFromMonth { get; set; }
        public int AgeFromYear { get; set; }
        public int AgeToDay { get; set; }
        public int AgeToMonth { get; set; }
        public int AgeToYear { get; set; }
        public bool NeedExpert { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool AgeFromInclusive { get; set; }
        public bool AgeToInclusive { get; set; }
    }
}
