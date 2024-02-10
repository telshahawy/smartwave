using System;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetReasonsKeyValueQuery : IGetReasonsKeyValueQuery
    {
        public int VisitTypeActionId { get; set; }

        public Guid ClientId { get; set; }
    }
}
