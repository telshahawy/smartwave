using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateGovernateCommand
    {
        Guid GovernateId { get; set; }
        Guid CountryId { get; set; }
        string GovernateNameEn { get; set; }
        string GovernateNameAr { get; set; }
        int Code { get; set; }
        string CustomerServiceEmail { get; set; }
        bool IsActive { get; set; }
        Guid CreatedBy { get; set; }
       
    }
}
