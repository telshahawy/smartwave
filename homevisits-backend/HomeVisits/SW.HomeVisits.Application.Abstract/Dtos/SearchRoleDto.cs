using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SearchRoleDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
    }
}
