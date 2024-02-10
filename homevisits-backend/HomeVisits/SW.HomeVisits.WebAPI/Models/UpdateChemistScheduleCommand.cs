using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateChemistScheduleCommand : IUpdateChemistScheduleCommand
    {
        public Guid ChemistScheduleId {get;set;}

        public Guid AssignedChemistGeoZoneId {get;set;}

        public float StartLatitude {get;set;}

        public float StartLangitude {get;set;}

        public DateTime StartDate {get;set;}

        public DateTime EndDate {get;set;}

        public TimeSpan? SunStart {get;set;}

        public TimeSpan? SunEnd {get;set;}

        public TimeSpan? MonStart {get;set;}

        public TimeSpan? MonEnd {get;set;}

        public TimeSpan? TueStart {get;set;}

        public TimeSpan? TueEnd {get;set;}

        public TimeSpan? WedStart {get;set;}

        public TimeSpan? WedEnd {get;set;}

        public TimeSpan? ThuStart {get;set;}

        public TimeSpan? ThuEnd {get;set;}

        public TimeSpan? FriStart {get;set;}

        public TimeSpan? FriEnd {get;set;}

        public TimeSpan? SatStart {get;set;}

        public TimeSpan? SatEnd {get;set;}
    }
}
