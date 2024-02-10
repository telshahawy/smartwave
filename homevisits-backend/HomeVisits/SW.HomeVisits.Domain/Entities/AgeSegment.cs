using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class AgeSegment:Entity<Guid>
    {
        public Guid AgeSegmentId { get=>Id; set=> Id = value; }
        public int Code { get; set; }
        public Guid ClientId { get; set; }
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
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Client Client { get; set; }
    }
}