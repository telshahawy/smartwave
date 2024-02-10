using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetSystemPagePermissionQuery : IGetSystemPagePermissionQuery
    {
        public int? SystemPageId { get; set; }
        public string SystemPageCode { get; set; }
        public int? SystemPagePermissionId { get; set; }
        public int? PermissionId { get; set; }
        public int? PermissionCode { get; set; }
        public CultureNames CultureName { get; set; }
    }
}
