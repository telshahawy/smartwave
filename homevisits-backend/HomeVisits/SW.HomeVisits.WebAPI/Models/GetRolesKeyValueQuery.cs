using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetRolesKeyValueQuery : IGetRolesKeyValueQuery
    {
        public Guid ClientId { get; set; }
    }
}
