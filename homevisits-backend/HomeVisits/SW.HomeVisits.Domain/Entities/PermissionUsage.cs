using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class PermissionUsage : Entity<int>
    {
        public int PermissionUsageId { get => Id; set => Id = value; }
        public int PermissionId { get; set; }
        public int Usage { get; set; }
    }
}
