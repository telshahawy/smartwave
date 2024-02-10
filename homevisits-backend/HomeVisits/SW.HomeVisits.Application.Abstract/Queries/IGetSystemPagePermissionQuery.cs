using SW.HomeVisits.Application.Abstract.Enum;
using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetSystemPagePermissionQuery
    {
        public int? SystemPageId { get; set; }
        public string SystemPageCode { get; set; }
        public int? SystemPagePermissionId { get; set; }
        public int? PermissionId { get; set; }
        public int? PermissionCode { get; set; }
        CultureNames CultureName { get; }
    }
}
