using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class RolePermission : Entity<Guid>
    {
        public Guid RolePermissionId { get => Id; set => Id = value; }
        public Guid RoleId { get; set; }
        public int SystemPagePermissionId { get; set; }
        public bool IsDeleted { get; set; }
        public SystemPagePermission SystemPagePermission { get; set; }
    }
}
