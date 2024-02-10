using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SW.HomeVisits.BackGroundService.AssignAndOptimizeVisits;
using System;

namespace SW.HomeVisits.BackGroundService.DependencyInjection
{
    public static class BackGroundServiceExtention
    {
        public static void AddBackGroundServiceConfigration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAssignAndOptimizeVisitsJobService, AssignAndOptimizeVisitsJobService>();
            services.AddScoped<IAssignAndOptimizeVisitsRecurringJob, AssignAndOptimizeVisitsRecurringJob>();

            services.AddHangfire(hangfireconfiguration => hangfireconfiguration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetSection("AppSettings").GetValue<string>("ConnectionString"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            }));
            services.AddHangfireServer();
        }

        public static void UseAssignAndOptimizeVisitsRecurringJob(this IApplicationBuilder app, IAssignAndOptimizeVisitsRecurringJob assignAndOptimizeVisitsRecurringJob)
        {
            assignAndOptimizeVisitsRecurringJob.AssignAndOptimizeVisits();
        }
    }
}
