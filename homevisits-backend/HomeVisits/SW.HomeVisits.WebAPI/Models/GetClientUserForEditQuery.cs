using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetClientUserForEditQuery : IGetClientUserForEditQuery
    {
        public Guid UserId { get; set; }

        public Guid ClientId { get; set; }
    }
}
