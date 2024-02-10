using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetSystemPagesWithPermissionsTreeQuery: IGetSystemPagesWithPermissionsTreeQuery
    {
        public CultureNames CultureName { get; set; }
    }
}
