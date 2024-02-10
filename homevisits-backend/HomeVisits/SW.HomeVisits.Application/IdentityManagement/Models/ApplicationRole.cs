using System;
using Microsoft.AspNetCore.Identity;

namespace SW.HomeVisits.Application.IdentityManagement.Models
{
    public class ApplicationRole:IdentityRole<Guid>
    {
        public ApplicationRole()
        {
        }
    }
}
