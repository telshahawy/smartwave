using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SeachChemistsQuery : ISearchChemistsQuery
    {
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

        public CultureNames cultureName { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }

        public Guid ClientId { get; set; }
    }
}
