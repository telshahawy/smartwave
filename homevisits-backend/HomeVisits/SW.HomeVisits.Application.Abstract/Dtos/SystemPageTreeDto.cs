using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SystemPageTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool HasURL { get; set; }
        public int? ParentId { get; set; }
        public bool IsDisplayInMenue { get; set; }
        public int? Position { get; set; }
        public List<PermissionTreeDto> Permissions { get; set; } = new List<PermissionTreeDto>();
        public List<PermissionTree> SubChild { get; set; } = new List<PermissionTree>();

    }
    public class PermissionTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PermissionTreeDto
    {
        public int SystemPagePermissionId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public int PermissionCode { get; set; }
        public int? PermissionPosition { get; set; }
    }
}
