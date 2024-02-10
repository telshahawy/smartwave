using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class MyScheduleDto
    {
        public Guid ChemistId { get; set; }
        public string Name { get; set; }
        public string GenderName { get; set; }
        public int Gender { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string Street { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int VisitStatusTypeId { get; set; }
        public string StatusName { get; set; }
        public string VisitNo { get; set; }
        public DateTime VisitDate { get; set; }
        public string Zone { get; set; }
        public string GoverName { get; set; }
        public string VisitDateString { get; set; }
        public string VisitTime { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Building { get; set; }
        public Guid GeoZoneId { get; set; }
        public Guid VisitId { get; set; }
        public Guid PatientId { get; set; }
        public string AddressFormatted { get; set; }
        public bool ExpertChemist { get; set; }
        public Guid ClientId { get; set; }
    }
}
