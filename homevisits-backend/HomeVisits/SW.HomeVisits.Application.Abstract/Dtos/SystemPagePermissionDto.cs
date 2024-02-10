using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SystemPagePermissionDto
    {
        public int SystemPagePermissionId { get; set; }
        public int SystemPageId { get; set; }
        public int PermissionId { get; set; }
        public string SystemPageCode { get; set; }
        public string SystemPageName { get; set; }
        public int SystemPagePosition { get; set; }
        public bool HasURL { get; set; }
        public int? ParentId { get; set; }
        public bool IsDisplayInMenue { get; set; }
        public int PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public int PermissionPosition { get; set; }
        public bool PermissionIsActive { get; set; }
    }
}
