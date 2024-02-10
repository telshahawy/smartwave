using System;
using System.Collections.Generic;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateCountryModel
    {
        public string CountryNameEn { get; set; }
        public bool IsActive { get; set; }
        public int MobileNumberLength { get; set; }
    }
}
