using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetGovernateForEditQuery : IGetGovernateForEditQuery
    {
        public Guid GovernateId { get; set; }
    }
}
