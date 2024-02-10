using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateGovernateCommand
    {
        Guid GovernateId { get; set; }
        Guid CountryId { get; set; }
        string GovernateNameEn { get; set; }
        bool IsActive { get; set; }
        string CustomerServiceEmail { get; set; }
    }
}
