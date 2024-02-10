using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateCountryModel
    {
        public string CountryNameEn { get; set; }
        public int MobileNumberLength { get; set; }
        public bool IsActive { get; set; }
    }
}
