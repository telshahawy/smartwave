using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(RolesPermissionView), Schema = "HomeVisits")]
    public class RolesPermissionView
    {
        [Key]
        public Guid RolePermissionId { get; set; }
        public Guid RoleId { get; set; }
        public int SystemPagePermissionId { get; set; }
        public int SystemPageId { get; set; }
        public int PermissionId { get; set; }
        public string SystemPageCode { get; set; }
        public int PermissionCode { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public int? PermissionPosition { get; set; }
        public bool? PermissionIsActive { get; set; }
        public bool PermissionIsDeleted { get; set; }
        public bool SystemPageIsDisplayInMenue { get; set; }
    }
}
