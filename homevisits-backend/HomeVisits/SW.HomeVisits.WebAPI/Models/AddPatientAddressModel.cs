using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddPatientAddressModel
    {
        public Guid PatientId { get; set; }
        public string street { get; set; }
        public string LocationUrl { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Building { get; set; }
        public string AdditionalInfo { get; set; }
        public Guid GeoZoneId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
