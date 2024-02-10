using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SW.HomeVisits.Application.IdentityManagement.Models;
using SW.HomeVisits.Application.Abstract.Authentication;
using SW.HomeVisits.Infrastructure.AspIdentity.Authentication;
using SW.HomeVisits.Application.IdentityManagement;
using SW.HomeVisits.Domain.Entities;
using Microsoft.AspNet.Identity;
using SW.HomeVisits.Application.Configuration;

namespace SW.HomeVisits.Infrastructure.AspIdentity
{
    public class HomeVisitsIdentityModule
    {
        public void Initialise(IServiceCollection services)
        {
            var migrationsAssembly = typeof(HomeVisitsIdentityDbContext).GetTypeInfo().Assembly.GetName().Name;
            //const string connectionString = @"Data Source=.;Initial Catalog=HomeVisitsDBLiveLst;User ID=smart;Password=123456";
            //const string connectionString = @"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDB_CR;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#";
            //const string connectionString = @"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBStaging;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#";
            //const string connectionString = @"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBTest;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#";
            //const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBLive;User ID=hvdblive;Password=CzHlzXGa97T302t";
            //const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBUAT;User ID=hvdbuat;Password=up7zYatHtEX2NH4";
            string connectionString = DataBaseConnectionString.GetDataConnectionStringFromConfig();

            services.AddDbContext<HomeVisitsIdentityDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            })
                //.AddEntityFrameworkStores<HomeVisitsIdentityDbContext>()
                .AddUserStore<HomeVisitsUserStore>()
                .AddRoleStore<HomeVisitsRoleStore>()
                //.AddUserManager<Microsoft.AspNetCore.Identity.UserManager<User>>()
                .AddDefaultTokenProviders();
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            //services.AddTransient<Microsoft.AspNetCore.Identity.IUserStore<User>, HomeVisitsUserStore>();
            //services.AddTransient<Microsoft.AspNetCore.Identity.IRoleStore<Role>, HomeVisitsRoleStore>();
        }
    }
}
