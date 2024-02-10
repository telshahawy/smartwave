using SW.HomeVisits.Application.Abstract.Enum;
using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetUserSystemPageWithPermissionsTreeQuery
    {
        public Guid UserId { get; set; }
        public Guid? ClientId { get; set; }
        CultureNames CultureName { get; }
    }
}
