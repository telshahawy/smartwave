using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateGovernateCommand : IUpdateGovernateCommand
    {
        public Guid GovernateId { get; set; }
        public Guid CountryId { get; set; }
        public string GovernateNameEn { get; set; }
        public bool IsActive { get; set; }
        public string CustomerServiceEmail { get; set; }
    }
}
