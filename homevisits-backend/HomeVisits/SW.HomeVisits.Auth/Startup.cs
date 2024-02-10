using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SW.Framework.DependencyInjectionCore;
using SW.HomeVisits.Application.Abstract;
using SW.HomeVisits.Application;
using SW.HomeVisits.Infrastruture.Data;
using SW.HomeVisits.Infrastructure.ReadModel;
using SW.Framework.LoggingCore;
using SW.HomeVisits.Infrastructure.AspIdentity;
using SW.HomeVisits.Infrastructure.IdentityServer;
using SW.HomeVisits.Auth.Helper;
using SW.HomeVisits.Auth.Midelwares;
using Microsoft.AspNetCore.Http;
using SW.HomeVisits.Infrastructure.Notifications;
using Microsoft.AspNetCore.HttpOverrides;

namespace SW.HomeVisits.Auth
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IdentityServerModule _identityServerModule;
        readonly string AllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _hostingEnvironment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyHeader()
                                                  .AllowAnyMethod();

                                  });
            });
            services.AddControllersWithViews();
            services.AddSameSiteCookiePolicy();
            ConfigureApplication(services);
            services.AddAuthentication();
        }
        private void ConfigureApplication(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<EzagelSmsConfiguration>(Configuration.GetSection("EzagelSmsConfiguration"));
            services.AddSingleton<IHomeVisitsConfigurationProvider, HomeVisitsConfigurationProvider>();
            services.RegisterDefaultLogger(Configuration.GetSection("AppSettings").GetSection("LogPath").Value);
            InitializeModules(services);
            services.RegisterDefaultCommandBus();
            services.RegisterDefaultQueryProcessor();
            services.AddSingleton<ICultureService, CultureService>();
        }
        private void InitializeModules(IServiceCollection services)
        {
            (new HomeVisitsApplicationModule()).Initialize(services);
            (new HomeVisitsDomainDataModule()).Initialise(services);
            (new HomeVisitsReadModelModule()).Initialise(services);
            (new HomeVisitsIdentityModule()).Initialise(services);
            _identityServerModule = new IdentityServerModule();
            _identityServerModule.Initialise(services);
            (new NotificationModule()).Initialise(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _identityServerModule.InitializeIdentityDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardOptions);
            app.UseErrorHandlerMiddleware();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
           
        }
    }
}
