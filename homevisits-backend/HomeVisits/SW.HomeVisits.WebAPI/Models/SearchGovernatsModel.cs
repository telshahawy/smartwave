using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchGovernatsModel : IPaggingQuery
    {
        public int? Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CountryId { get; set; }
        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
