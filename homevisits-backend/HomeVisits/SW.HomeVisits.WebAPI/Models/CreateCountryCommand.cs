using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateCountryCommand : ICreateCountryCommand
    {
        public Guid CountryId { get; set; }
        public Guid ClientId { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public int Code { get; set; }
        public int MobileNumberLength { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
