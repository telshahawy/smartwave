using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class RejectedVisitReportDto
    {
        public string VisitId { get; set; }
        public string VisitDate { get; set; }
        public string PatientName { get; set; }
        public string MobileNumber { get; set; }
        public string Area { get; set; }
        public string RejectionType { get; set; }
        public string ChemistName { get; set; }
        public string Reason { get; set; }
    }
}
