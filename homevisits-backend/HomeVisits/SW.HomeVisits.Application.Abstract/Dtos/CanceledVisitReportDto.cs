
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class CanceledVisitReportDto
    {
        public string VisitId { get; set; }
        public string VisitDate { get; set; }
        public string PatientName { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Area { get; set; }
        public string CancellationTime { get; set; }
        public string CancelledBy { get; set; }
        public string CancellationReason { get; set; }
    
    }
}
