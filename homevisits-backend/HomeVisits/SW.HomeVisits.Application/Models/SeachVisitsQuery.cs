using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Models
{
    public class SeachVisitsQuery : ISearchVisitsQuery
    {
        public DateTime? VisitDateFrom { get; set; }
        public DateTime? VisitDateTo { get; set; }
        public TimeSpan? TimeZoneStartTime { get; set; }
        public TimeSpan? TimeZoneEndTime { get; set; }
        public Guid? TimeZoneGeoZoneId { get; set; }
        public int? VisitNoFrom { get; set; }
        public int? VisitNoTo { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public Guid? GovernateId { get; set; }
        public Guid? GeoZoneId { get; set; }
        public string PatientNo { get; set; }
        public string PatientName { get; set; }
        public int? Gender { get; set; }
        public string PatientMobileNo { get; set; }
        public int? VisitStatusTypeId { get; set; }
        public bool? NeedExpert { get; set; }
        public Guid? ClientId { get; set; }
        public CultureNames cultureName { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPageIndex { get; set; }
        public int? SortBy { get; set; }
        public int? AssignStatus { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime? VisitDate { get; set; }
        public Guid? PatientId { get; set; }
    }
}
