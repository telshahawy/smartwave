using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(UserPermissionView), Schema = "HomeVisits")]
    public class UserPermissionView
    {
        [Key]
        public Guid UserId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid ClientId { get; set; }
        public int SystemPagePermissionId { get; set; }
        public int SystemPageId { get; set; }
        public int PermissionId { get; set; }
        public string SystemPageCode { get; set; }
        public int PermissionCode { get; set; }
        public string SystemPageNameAr { get; set; }
        public string SystemPageNameEn { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public int? SystemPagePosition { get; set; }
        public int? PermissionPosition { get; set; }
        public bool? PermissionIsActive { get; set; }
        public bool PermissionIsDeleted { get; set; }
        public bool SystemPageHasURL { get; set; }
        public int? SystemPageParentId { get; set; }
        public bool SystemPageIsDisplayInMenue { get; set; }
    }
}
