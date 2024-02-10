using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SW.HomeVisits.Application;
using SW.HomeVisits.Application.Configuration;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Presistance;
using SW.HomeVisits.Infrastruture.Presistance.DbInitializer;
using SW.HomeVisits.Infrastruture.Presistance.Repositories;

namespace SW.HomeVisits.Infrastruture.Data
{
    public class HomeVisitsDomainDataModule
    {
        public void Initialise(IServiceCollection services)
        {
            RegisterDataArtifacts(services);
            RegisterRepositories(services);

        }

        private void RegisterDataArtifacts(IServiceCollection serviceCollections)
        {
            serviceCollections.AddScoped<IHomeVisitsUnitOfWork, HomeVisitsUnitOfWork>();
            serviceCollections.AddDbContext<HomeVisitsDomainContext>(options =>
                options.UseSqlServer(DataBaseConnectionString.GetDataConnectionStringFromConfig(), builder =>
                {
                    //builder.MigrationsAssembly(typeof(HomeVisitsDomainContext).GetTypeInfo().Assembly.FullName);
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));//.EnableSensitiveDataLogging());
            serviceCollections.AddScoped<IDbInitializer, DbInitializer>();
        }

        //public void UseAutoMigrateDatabase(IApplicationBuilder builder)
        //{
        //    using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        serviceScope.ServiceProvider.GetService<HomeVisitsDomainContext>().Database.Migrate();
        //    }
        //}

        private void RegisterRepositories(IServiceCollection serviceCollections)
        {
            serviceCollections.AddScoped<IUserRepository, UserRepository>();
            serviceCollections.AddScoped<IRoleRepository, RoleRepository>();
            serviceCollections.AddScoped<INotificationRepository, NotificationRepository>();
            serviceCollections.AddScoped<IChemistTrackingLogRepository, ChemistTrackingLogRepository>();
            serviceCollections.AddScoped<IPatientRepository, PatientRepository>();
            serviceCollections.AddScoped<ILostVisitTimeRepository, LostVisitTimeRepository>();
            serviceCollections.AddScoped<IVisitRepository, VisitRepository>();
            serviceCollections.AddScoped<IReasonRepository, ReasonRepository>();
            serviceCollections.AddScoped<ICountryRepository, CountryRepository>();
            serviceCollections.AddScoped<IGovernatRepository, GovernatRepository>();
            serviceCollections.AddScoped<IGeoZonesRepository, GeoZonesRepository>();
            serviceCollections.AddScoped<IAgeSegmentsRepository, AgeSegmentsRepository>();
            serviceCollections.AddScoped<ISystemParametersRepository, SystemParametersRepository>();
            serviceCollections.AddScoped<IClientRepository, ClientRepository>();
            serviceCollections.AddScoped<IChemistVisitOrderRepository, ChemistVisitOrderRepository>();
            serviceCollections.AddScoped<ISystemPagePermissionRepository, SystemPagePermissionRepository>();
        }

    }

}