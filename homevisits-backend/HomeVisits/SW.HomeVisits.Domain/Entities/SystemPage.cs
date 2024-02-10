using System;
using System.Collections.Generic;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class SystemPage : Entity<int>
    {
        public int SystemPageId { get => Id; set => Id = value; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? Position { get; set; }
        public int? ParentId { get; set; }
        public bool HasURL { get; set; } // if true that mean it is page, else that mean it is menue or sub menue
        public bool IsDisplayInMenue { get; set; }
        public virtual ICollection<SystemPagePermission> SystemPagePermissions { get; set; }
    }
}
