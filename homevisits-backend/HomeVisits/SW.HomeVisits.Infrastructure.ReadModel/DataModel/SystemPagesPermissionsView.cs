using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(SystemPagesPermissionsView), Schema = "HomeVisits")]
    public class SystemPagesPermissionsView
    {
        public int SystemPagePermissionId { get; set; }
        public int SystemPageId { get; set; }
        public int PermissionId { get; set; }
        public string SystemPageCode { get; set; }
        public string SystemPageNameAr { get; set; }
        public string SystemPageNameEn { get; set; }
        public int SystemPagePosition { get; set; }
        public bool HasURL { get; set; }
        public int? ParentId { get; set; }
        public bool IsDisplayInMenue { get; set; }
        public int PermissionCode { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissonNameEn { get; set; }
        public int? PermissionPosition { get; set; }
        public bool? PermissionIsActive { get; set; }
    }
}
