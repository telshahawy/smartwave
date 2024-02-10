using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddPatientAddressCommand: IAddPatientAddressCommand
    {
        public Guid PatientId { get; set; }
        public Guid PatientAddressId { get; set; }
        public string street { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationUrl { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Building { get; set; }
        public string AdditionalInfo { get; set; }
        public Guid GeoZoneId { get; set; }
        public Guid CreateBy { get; set; }
    }
}
