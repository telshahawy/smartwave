using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ClientView), Schema = "HomeVisits")]
    public class ClientView
    {
        [Key]
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
