using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public Guid CountryId { get; set; }
        public string URLName { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
