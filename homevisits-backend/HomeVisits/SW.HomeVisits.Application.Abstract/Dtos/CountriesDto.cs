using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class CountriesDto
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Code { get; set; }
        public int MobileNumberLength { get; set; }
    }
}