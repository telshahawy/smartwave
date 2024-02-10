using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchVisitsQuery : IPaggingQuery
    {
        DateTime? VisitDateFrom { get; }
        DateTime? VisitDateTo { get; }
        public TimeSpan? TimeZoneStartTime { get; set; }
        public TimeSpan? TimeZoneEndTime { get; set; }
        public Guid? TimeZoneGeoZoneId { get; set; }
        DateTime? VisitDate{ get; }
        int? VisitNoFrom { get; }
        int? VisitNoTo { get; }
        DateTime? CreationDateFrom { get; }
        DateTime? CreationDateTo { get; }
        Guid? GovernateId { get; }
        Guid? GeoZoneId { get; }
        Guid? PatientId { get; }
        string PatientNo { get; }
        string PatientName { get; }
        int? Gender { get; }
        string PatientMobileNo { get; }
        int? VisitStatusTypeId { get; }
        bool? NeedExpert { get; }
        Guid? ClientId { get; }
        CultureNames cultureName { get; }
        public int? SortBy { get; set; }
        public int? AssignStatus { get; set; }
        public Guid? AssignedTo { get; set; }
    }
}
