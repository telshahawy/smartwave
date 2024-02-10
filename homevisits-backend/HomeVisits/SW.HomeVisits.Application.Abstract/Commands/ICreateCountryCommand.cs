using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateCountryCommand
    {
        Guid CountryId { get; set; }
        Guid ClientId { get; set; }
        string CountryNameEn { get; set; }
        string CountryNameAr { get; set; }
        int Code { get; set; }
        int MobileNumberLength { get; set; }
        bool IsActive { get; set; }
        Guid CreatedBy { get; set; }
       
    }
}
