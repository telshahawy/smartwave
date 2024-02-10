using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class PatientAddress:Entity<Guid>
    {
        public Guid PatientAddressId
        {
            get => Id;
            set => Id = value;
        }
       
        public string street { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationUrl { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Building { get; set; }
        public Guid PatientId { get; set; }
        public string AdditionalInfo { get; set; }
        public Guid GeoZoneId { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ConfirmedBy { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int Code { get; set; }
        public GeoZone GeoZone { get; set; }
    }
}