using System;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.WebAPI.Helper;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateChemistScheduleModel
    {
        public Guid ChemistId {get;set;}
         public Guid GeoZoneId {get;set;}
        public string ChemistName { get; set; }
        public Guid AssignedChemistGeoZoneId {get;set;}

        public float StartLatitude {get;set;}

        public float StartLangitude {get;set;}
        public DateTime StartDate {get;set;}

        public DateTime EndDate {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? SunStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? SunEnd {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? MonStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? MonEnd {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? TueStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? TueEnd {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? WedStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? WedEnd {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? ThuStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? ThuEnd {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? FriStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? FriEnd {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? SatStart {get;set;}
        [System.Text.Json.Serialization.JsonConverterAttribute(typeof(TimeSpanConverter))]
        public TimeSpan? SatEnd {get;set;}
    }
}
