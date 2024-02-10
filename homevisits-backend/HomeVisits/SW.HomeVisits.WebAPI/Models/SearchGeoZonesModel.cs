using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchGeoZonesModel : IPaggingQuery
    {
        public int? Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? GovernateId { get; set; }
        public string MappingCode { get; set; }
        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
