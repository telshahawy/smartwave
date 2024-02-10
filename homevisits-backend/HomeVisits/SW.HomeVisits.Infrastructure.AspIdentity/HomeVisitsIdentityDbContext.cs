using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SW.HomeVisits.Application.IdentityManagement.Models;

namespace SW.HomeVisits.Infrastructure.AspIdentity
{
    public class HomeVisitsIdentityDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public HomeVisitsIdentityDbContext(DbContextOptions<HomeVisitsIdentityDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
