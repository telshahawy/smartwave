using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateCountryCommand : IUpdateCountryCommand
    {
        public Guid CountryId { get; set; }
        public string CountryNameEn { get; set; }
        public bool IsActive { get; set; }
        public int MobileNumberLength { get; set; }
        public Guid ClientId { get; set; }
    }
}
