using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetVisitDetailsQuery
    {
        public Guid VisitId { get; set; }
        CultureNames CultureName { get; }
    }
}
