using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchChemistsModel:IPaggingQuery
    {
        public int? PageSize { get; set; }
        public int? CurrentPageIndex { get; set; }
        public DateTime? JoinDateFrom { get; set; }
        public DateTime? JoinDateto { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? GovernateId { get; set; }
        public Guid? GeoZoneId { get; set; }
        public int? Code { get; set; }
        public string ChemistName { get; set; }
        public int? Gender { get; set; }
        public string PhoneNo { get; set; }
        public bool? AreaAssignStatus { get; set; }
        public bool? ChemistStatus { get; set; }
        public bool? ExpertChemist { get; set; }
    }
}
