using SW.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Domain.Entities
{
    public class SystemPagePermission : Entity<int>
    {
        public int SystemPagePermissionId { get => Id; set => Id = value; }
        public int SystemPageId { get; set; }
        public int PermissionId { get; set; }
        public string NameAr { get; set; } //Fill it if you need to add specific name override name found in permission table
        public string NameEn { get; set; } //Fill it if you need to add specific name override name found in permission table
        public virtual Permission Permission { get; set; }
        public virtual SystemPage SystemPage { get; set; }
    }
}
