using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistSchedulePlanDto
    {
        public Guid ChemistId { get; set; }
        public string Code { get; set; }
        public int DOB { get; set; }
        public bool ExpertChemist { get; set; }
        public DateTime JoinDate { get; set; }
        public Guid ChemistAssignedGeoZoneId { get; set; }
        public Guid GeoZoneId { get; set; }
        public Guid ChemistScheduleId { get; set; }
        public DateTime ScheuleStartDate { get; set; }
        public DateTime ScheduleEndDate { get; set; }
        public float StartLatitude { get; set; }
        public float StartLangitude { get; set; }
        public Guid ChemistScheduleDayId { get; set; }
        public int Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid ClientId { get; set; }

    }
}
