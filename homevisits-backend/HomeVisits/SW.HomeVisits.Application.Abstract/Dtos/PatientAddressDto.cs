using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class PatientAddressDto
    {
        public Guid PatientAddressId { get; set; }
        public int Code { get; set; }
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Floor { get; set; }

        public string Flat { get; set; }

        public Guid GeoZoneId { get; set; }

        public string Building { get; set; }

        public string street { get; set; }

        public bool IsConfirmed { get; set; }

        public string LocationUrl { get; set; }

        public string KmlFilePath { get; set; }

        public Guid GovernateId { get; set; }

        public Guid CountryId { get; set; }

        public string ZoneName { get; set; }
        public string GovernateName { get; set; }
        public string CountryName { get; set; }

        public DateTime AddressCreatedAt { get; set; }
        public string AddressFormatted { get; set; }
        //public bool? IsAddressVerified { get; set; }


    }
}
