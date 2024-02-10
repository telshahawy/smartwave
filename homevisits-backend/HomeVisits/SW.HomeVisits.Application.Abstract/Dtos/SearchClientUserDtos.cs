using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class SearchClientUserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
    }
}
