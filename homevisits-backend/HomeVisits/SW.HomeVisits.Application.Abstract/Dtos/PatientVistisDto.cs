using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class PatientVistisDto
    {
        public Guid VisitId { get; set; }
        public string VisitNo { get; set; }
        public int VisitCode { get; set; }
        public DateTime VisitDate { get; set; }
        public Guid GeoZoneId { get; set; }
        public int VisitStatusTypeId { get; set; }
        public Guid PatientId { get; set; }
        public string ZoneName { get; set; }
        public string StatusName { get; set; }
        public string VisitDateString { get; set; }
        public string VisitTime { get; set; }

    }
}
