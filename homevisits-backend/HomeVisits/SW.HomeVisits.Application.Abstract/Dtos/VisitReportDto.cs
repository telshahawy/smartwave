using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class VisitReportDto
    {
        public int VisitNo { get; set; }
        public string VisitDate { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public string GovernorateNameEn { get; set; }
        public string GovernorateNameAr { get; set; }
        public string AreaNameEn { get; set; }
        public string AreaNameAr { get; set; }

        public string ChemistNameEn { get; set; }
        public string ChemistNameAr { get; set; }

        public string Delayed { get; set; }//no,yes

    }
}
