using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateGovernateModel
    {
        public string GovernateNameEn { get; set; }
        public Guid CountryId { get; set; }
        public string CustomerServiceEmail { get; set; }
        public bool IsActive { get; set; }
    }
}
