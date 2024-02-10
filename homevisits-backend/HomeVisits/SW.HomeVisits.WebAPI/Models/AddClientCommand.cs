using SW.HomeVisits.Application.Abstract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddClientCommand:IAddClientCommand
    {
        public Guid ClientId { get; set; }
        public Guid CountryId { get; set; }
        public string ClientName { get; set; }
        public string ClientCode { get; set; }
        public string URLName { get; set; }

        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }
    }
}
