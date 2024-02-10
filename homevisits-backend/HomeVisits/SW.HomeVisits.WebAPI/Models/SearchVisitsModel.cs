using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchVisitsModel : IPaggingQuery
    {
        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
        public DateTime? VisitDateFrom { get; set; }
        public DateTime? VisitDateTo { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitNoFrom { get; set; }
        public int? VisitNoTo { get; set; }
        public DateTime? CreationDateFrom { get; set; }
        public DateTime? CreationDateTo { get; set; }
        public Guid? GovernateId { get; set; }
        public Guid? GeoZoneId { get; set; }
        public Guid? PatientId { get; set; }
        public string PatientNo { get; set; }
        public string PatientName { get; set; }
        public int? Gender { get; set; }
        public string PatientMobileNo { get; set; }
        public int? VisitStatusTypeId { get; set; }
        public bool? NeedExpert { get; set; }
        public int? SortBy { get; set; }
        public int? AssignStatus { get; set; }
        public Guid? AssignedTo { get; set; }
    }
}
