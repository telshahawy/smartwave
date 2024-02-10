using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class GeoZone : Entity<Guid>
    {
        public Guid GeoZoneId
        {
            get => Id;
            set =>Id = value;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string KmlFileName { get; set; }
        public string KmlFilePath { get; set; }
        public string MappingCode { get; set; }
        public Guid GovernateId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public Governate Governate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<TimeZoneFrame> TimeZones { get; set; }
        public int Code { get; set; }
    }
}