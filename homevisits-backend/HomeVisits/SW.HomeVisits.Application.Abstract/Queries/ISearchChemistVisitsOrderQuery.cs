using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchChemistVisitsOrderQuery : IPaggingQuery
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
        CultureNames cultureName { get; }
    }
}
