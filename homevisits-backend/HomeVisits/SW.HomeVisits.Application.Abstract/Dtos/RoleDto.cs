using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public Guid? ClientId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int DefaultPageId { get; set; }
        public List<int> Permissions { get; set; } = new List<int>();
        public List<Guid> GeoZones { get; set; }
    }

    public class RolePermissionDto
    {
        public int PermissionId { get; set; }
        public bool IsChecked { get; set; }
    }
}
