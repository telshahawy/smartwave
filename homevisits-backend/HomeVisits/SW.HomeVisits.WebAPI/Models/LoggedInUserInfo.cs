using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class LoggedInUserInfo
    {
       public Guid UserId { get; set; }
        public Guid? ClientId { get; set; }
    }
}
