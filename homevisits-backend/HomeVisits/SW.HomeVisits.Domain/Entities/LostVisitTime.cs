using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class LostVisitTime : Entity<Guid>
    {
        public LostVisitTime(Guid lostVisitTimeId, Guid visitId, TimeSpan lostTime, Guid createdBy, DateTime createdOn)
        {
            LostVisitTimeId = lostVisitTimeId;
            VisitId = visitId;
            LostTime = lostTime;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
        }

        public Guid LostVisitTimeId
        {
            get => Id;
            set => Id = value;
        }

        public Guid VisitId { get; set; }
        public TimeSpan LostTime { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Visit Visit { get; set; }

    }
}
