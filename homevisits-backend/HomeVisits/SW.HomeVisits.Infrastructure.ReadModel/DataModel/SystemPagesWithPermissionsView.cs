using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(SystemPagesWithPermissionsView), Schema = "HomeVisits")]
    public class SystemPagesWithPermissionsView
    {
        [Key]
        public int SystemPageId { get; set; }
        [Key]
        public string SystemPageCode { get; set; }
        [Key]
        public string SystemPageNameAr { get; set; }
        [Key]
        public string SystemPageNameEn { get; set; }
        [Key]
        public int SystemPagePosition { get; set; }
        public bool SystemPageHasURL { get; set; }
        public int? SystemPageParentId { get; set; }
        public bool SystemPageIsDisplayInMenue { get; set; }
        public int? SystemPagePermissionId { get; set; }
        public int? PermissionId { get; set; }
        [Key]
        public int? PermissionCode { get; set; }
        [Key]
        public string PermissionNameAr { get; set; }
        [Key]
        public string PermissonNameEn { get; set; }
        [Key]
        public int? PermissionPosition { get; set; }
        [Key]
        public bool? PermissionIsActive { get; set; }
    }
}
