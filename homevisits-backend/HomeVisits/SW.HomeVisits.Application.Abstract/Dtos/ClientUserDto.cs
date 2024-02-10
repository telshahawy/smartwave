using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ClientUserDto
    {
        public Guid UserId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public Guid ClientId { get; set; }
        public List<Guid> GeoZonesIds { get; set; }
    }
}
