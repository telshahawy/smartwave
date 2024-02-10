using SW.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Domain.Entities
{
    public class UserExcludedRolePermission : Entity<Guid>
    {
        public Guid UserExcludedRolePermissionId { get => Id; set => Id = value; }
        public Guid RoleId { get; set; }
        public int SystemPagePermissionId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public Role Role { get; set; }
        public SystemPagePermission SystemPagePermission { get; set; }
    }
}
