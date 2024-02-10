using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Models
{
    public class SearchChemistVisitsOrderQuery : ISearchChemistVisitsOrderQuery
    {
        public Guid? ChemistVisitOrderId { get; set; }
        public Guid? VisitId { get; set; }
        public Guid? ChemistId { get; set; }
        public Guid? TimeZoneFrameId { get; set; }
        public Guid? ClientId { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitDateFrom { get; set; }
        public DateTime? VisitDateTo { get; set; }
        public DateTime? VisitCreatedDate { get; set; }
        public DateTime? VisitOrderCreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public CultureNames cultureName { get; }
        public int? PageSize { get; set; }
        public int? CurrentPageIndex { get; set; }
    }
}
