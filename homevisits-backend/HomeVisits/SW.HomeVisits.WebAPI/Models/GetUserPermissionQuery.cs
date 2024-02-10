using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetUserPermissionQuery : IGetUserPermissionQuery
    {
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
    }
}
