using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchMyScheduleQuery
    {
        public Guid ChemistId { get; set; }
        public string Order { get; set; }
        public string VisitDate { get; set; }
        CultureNames CultureName{get;}
        public Guid ClientId { get; set; }
        public int Status { get; set; }
    }
}
