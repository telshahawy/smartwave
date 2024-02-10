using SW.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Domain.Entities
{
    public class UserAdditionalPermission : Entity<Guid>
    {
        public Guid UserAdditionalPermissionId { get => Id; set => Id = value; }
        public Guid UserId { get; set; }
        public int SystemPagePermissionId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual SystemPagePermission SystemPagePermission { get; set; }
    }
}
