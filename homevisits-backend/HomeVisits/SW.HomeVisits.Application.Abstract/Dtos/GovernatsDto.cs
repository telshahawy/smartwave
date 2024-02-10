using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class GovernatsDto
    {
        public Guid GovernateId { get; set; }
        public string GovernateName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Code { get; set; }
        public string CountryName { get; set; }
        public string CustomerServiceEmail { get; set; }
        public Guid CountryId { get; set; }

    }
}