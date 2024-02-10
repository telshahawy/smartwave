using System;
using System.Collections.Generic;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateGovernateModel
    {
        public string GovernateNameEn { get; set; }
        public bool IsActive { get; set; }
        public string CustomerServiceEmail { get; set; }
        public Guid CountryId { get; set; }
    }
}
