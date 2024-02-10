using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddClientModel
    {
        public string ClientName { get; set; }
        public string ClientCode { get; set; }

        public Guid CountryId { get; set; }

        public string URLName { get; set; }

        public string DisplayName { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }




    }
}
