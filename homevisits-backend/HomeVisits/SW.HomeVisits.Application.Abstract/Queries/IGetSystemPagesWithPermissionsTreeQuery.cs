using SW.HomeVisits.Application.Abstract.Enum;
using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetSystemPagesWithPermissionsTreeQuery
    {
        CultureNames CultureName { get; }
    }
}
