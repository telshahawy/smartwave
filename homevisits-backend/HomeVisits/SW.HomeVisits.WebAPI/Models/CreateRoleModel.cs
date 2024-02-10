using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class CreateRoleModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int DefaultPageId { get; set; }
        public List<Guid> GeoZones { get; set; } = new List<Guid>();
        public List<int> Permissions { get; set; } = new List<int>();
    }
}
