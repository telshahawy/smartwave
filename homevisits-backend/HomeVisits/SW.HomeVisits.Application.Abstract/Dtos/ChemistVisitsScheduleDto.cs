using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistVisitsScheduleDto
    {
        public Guid VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitCode { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public Guid ChemistId { get; set; }
        public string StatusName { get; set; }
        public Guid PatientAddressId { get; set; }
        public string PatientAddress { get; set; }
        public string AreaName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
