using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateCountryCommand
    {
        Guid CountryId { get; set; }
        string CountryNameEn { get; set; }
        bool IsActive { get; set; }
        int MobileNumberLength { get; set; }
        Guid ClientId { get; set; }
    }
}
