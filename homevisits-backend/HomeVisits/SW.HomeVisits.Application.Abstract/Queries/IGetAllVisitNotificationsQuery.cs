using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetAllVisitNotificationsQuery
    {
        public Guid ChemistId { get; set; }
        CultureNames CultureName { get; }
    }
}
