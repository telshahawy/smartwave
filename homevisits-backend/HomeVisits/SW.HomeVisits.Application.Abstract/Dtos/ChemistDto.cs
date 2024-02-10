using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistDto
    {
        public Guid ChemistId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        //public Guid GovenateId { get; set; }
        public string GovenateName { get; set; }
        //public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        //public Guid GeoZoneId { get; set; }
        public string GeoZoneName { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public Guid ClientId { get; set; }

    }
}
