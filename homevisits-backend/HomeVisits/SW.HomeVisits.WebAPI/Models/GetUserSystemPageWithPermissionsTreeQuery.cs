using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetUserSystemPageWithPermissionsTreeQuery : IGetUserSystemPageWithPermissionsTreeQuery
    {
        public Guid UserId { get; set; }
        public Guid? ClientId { get; set; }
        public CultureNames CultureName { get; set; }
    }
}
