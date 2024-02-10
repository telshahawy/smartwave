using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateGovernateCommand : ICreateGovernateCommand
    {
        public Guid GovernateId { get; set; }
        public Guid CountryId { get; set; }
        public string GovernateNameEn { get; set; }
        public string GovernateNameAr { get; set; }
        public int Code { get; set; }
        public string CustomerServiceEmail { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
