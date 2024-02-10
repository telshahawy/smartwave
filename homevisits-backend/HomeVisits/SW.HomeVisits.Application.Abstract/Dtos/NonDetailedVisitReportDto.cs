using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class NonDetailedVisitReportDto
    {
        public string ChemistNameEn { get; set; }
        public string ChemistNameAr { get; set; }
        public int VisitsCount { get; set; }
        public int NonDelayedVisitsCount { get; set; }
        public int DelayedVisitsCount { get; set; }
    }
}
