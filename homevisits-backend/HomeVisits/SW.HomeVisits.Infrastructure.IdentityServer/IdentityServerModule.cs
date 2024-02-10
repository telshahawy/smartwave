using System;
using System.Linq;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SW.HomeVisits.Application.IdentityManagement.Models;
using SW.HomeVisits.Application.IdentityManagement;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Application.Configuration;

namespace SW.HomeVisits.Infrastructure.IdentityServer
{
    public class IdentityServerModule
    {
        public void Initialise(IServiceCollection services)
        {
            var migrationsAssembly = typeof(IdentityServerModule).GetTypeInfo().Assembly.GetName().Name;
            //const string connectionString = @"Data Source=.;Initial Catalog=HomeVisitsDBUAT2;User ID=smart;Password=123456";
            //const string connectionString = @"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDB_CR;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#";
            //const string connectionString = @"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBStaging;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#";
            //const string connectionString = @"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBTest;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#";
            //const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBUAT;User ID=hvdbuat;Password=up7zYatHtEX2NH4";
            //const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBLive;User ID=hvdblive;Password=CzHlzXGa97T302t";
            //const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBUAT;User ID=hvdbuat;Password=up7zYatHtEX2NH4";
            string connectionString = DataBaseConnectionString.GetDataConnectionStringFromConfig();

            var builder =
                services.AddIdentityServer()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    .AddOperationalStore(options =>
                    {

                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    }).AddAspNetIdentity<User>()
                    .AddProfileService<ProfileService>()
                    .AddDeveloperSigningCredential();
        }

        public void InitializeIdentityDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
