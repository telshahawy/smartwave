using System;
using SW.Framework.Domain;
namespace SW.HomeVisits.Domain.Entities
{
    public class OnHoldVisit:Entity<Guid>
    {
        public Guid OnHoldVisitId { get; set; }
        public Guid? ChemistId { get; set; }
        public string DeviceSerialNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCanceled { get; set; }
        public Guid TimeZoneFrameId { get; set; }
        public int NoOfPatients { get; set; }
     //   public Chemist Chemist { get; set; }
        public TimeZoneFrame TimeZoneFrame { get; set; }
    }
}
