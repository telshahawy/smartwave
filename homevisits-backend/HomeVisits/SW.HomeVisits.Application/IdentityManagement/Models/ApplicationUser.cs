using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.IdentityManagement.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public int UserType { get; set; }
        public Guid HomeVisitsClientId { get; set; }
        public string FullName { get; set; }
    }
}
