using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchChemistsQuery: IPaggingQuery
    {
        DateTime? JoinDateFrom { get; }
        DateTime? JoinDateto { get; }
        Guid? CountryId { get; }
        Guid? GovernateId { get; }
        Guid? GeoZoneId { get; }
        int? Code { get; }
        string ChemistName { get; }
        int? Gender { get; }
        string PhoneNo { get; }
        bool? AreaAssignStatus { get; }
        bool? ChemistStatus { get; }
        bool? ExpertChemist{ get; }
        Guid ClientId { get; }
        CultureNames cultureName { get; }
    }
}
