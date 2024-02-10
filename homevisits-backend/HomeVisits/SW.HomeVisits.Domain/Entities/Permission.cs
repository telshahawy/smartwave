using System;
using System.Collections.Generic;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Permission : Entity<int>
    {
        public int PermissionId { get => Id; set => Id = value; }
        public int Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? Position { get; set; }
        public bool IsActive { get; set; }
    }
}
