using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchAgeSegmentsModel : IPaggingQuery
    {
        public int? Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? NeedExpert { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPageIndex { get; set; }
    }
}
