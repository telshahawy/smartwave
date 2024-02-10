using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(PermissionView), Schema = "HomeVisits")]
    public class PermissionView
    {
        public int PermissionId { get; set; }
        public int Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? Position { get; set; }
        public bool IsActive { get; set; }
    }
}
