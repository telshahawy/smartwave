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
using SW.HomeVisits.WebApi;
using SW.HomeVisits.Application;
using SW.HomeVisits.Infrastruture.Data;
using SW.HomeVisits.Infrastructure.ReadModel;
using SW.Framework.LoggingCore;
using SW.HomeVisits.Infrastructure.AspIdentity;
using SW.HomeVisits.Infrastructure.IdentityServer;
using SW.HomeVisits.WebAPI.Helper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SW.HomeVisits.WebAPI.Midelwares;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using SW.HomeVisits.Infrastructure.Notifications;
using Microsoft.IdentityModel.Logging;
using Hangfire;
using Hangfire.SqlServer;
using SW.HomeVisits.WebAPI.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using SW.HomeVisits.BackGroundService.DependencyInjection;
using SW.HomeVisits.BackGroundService.AssignAndOptimizeVisits;
using SW.HomeVisits.Infrastruture.Presistance.DbInitializer;

namespace SW.HomeVisits.WebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IdentityServerModule _identityServerModule;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _hostingEnvironment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureApplication(services);

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "allow",
            //                      builder =>
            //                      {
            //                          builder.AllowAnyOrigin();
            //                          builder.AllowAnyHeader();
            //                          builder.AllowAnyOrigin();

            //                      });
            //});

            var jwtBearerAuthority = Configuration.GetSection("AppSettings").GetValue<string>("JwtBearerAuthority");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                //x.Authority = "https://hvdevapi.smartwaveeg.com";
                //x.Authority = "https://hvtestauth.smartwaveeg.com";
                //x.Authority = "http://localhost:52423";
                //x.Authority = "https://hvstagingauth.smartwaveeg.com";
                //x.Authority = "https://hvuat-auth.alfalaboratory.com";
                //x.Authority = "https://hvauth.alfalaboratory.com";
                x.Authority = jwtBearerAuthority;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateLifetime = false,
                    //SaveSigninToken = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = false,

                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && ((path.StartsWithSegments("/hubs/chat"))))
                        {
                            context.Token = accessToken;

                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        // Skip the default logic.
                        //context..HandleResponse();
                        var response = new HomeVisitsWebApiResponse<bool>
                        {
                            Message = "Unauthorized",
                            ResponseCode = WebApiResponseCodes.UnAuthorized,
                            ErrorList = new List<string>
                            {
                                 context.Error
                            }
                        };
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;
                        var payload = JsonConvert.SerializeObject(response);
                        return context.Response.WriteAsync(payload);
                    }
                };

            });
            IdentityModelEventSource.ShowPII = true;
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
                //options.AddPolicy("ApiScope", policy =>
                //{
                //    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                //    policy.RequireAuthenticatedUser();
                //    //policy.RequireClaim("scope", "api1");
                //});
            });
            services.AddTransient<SW.HomeVisits.WebAPI.Controllers.VisitController, SW.HomeVisits.WebAPI.Controllers.VisitController>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Chemist API",
                    Version = "v1",
                    Description = "Chemist API V1",
                });
            });

            BackGroundServiceExtention.AddBackGroundServiceConfigration(services, Configuration);
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
            services.AddTransient<IChemistTrackingService, ChemistTrackingService>();

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, IServiceProvider serviceProvider)
        {
            app.InitializeDBAndSeedDefualtData();
            //(new HomeVisitsDomainDataModule()).UseAutoMigrateDatabase(app);
            _identityServerModule.InitializeIdentityDatabase(app);

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseErrorHandlerMiddleware();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                               Path.Combine(Directory.GetCurrentDirectory(), @"Uploads/UsersPhotos")),
                RequestPath = new PathString("/Uploads/UsersPhotos")
            });

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            RecurringJob.Trigger("Auto-CancelVisitorAlertList");

            RecurringJob.AddOrUpdate(
                "Chemist Tracking Service",
                () => serviceProvider.GetService<IChemistTrackingService>().ChemistTrackingBackGroundService(),
                "*/20 * * * *"
                );

            app.UseAssignAndOptimizeVisitsRecurringJob(serviceProvider.GetService<IAssignAndOptimizeVisitsRecurringJob>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization("Authenticated");
                endpoints.MapHangfireDashboard();
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Chemist API V1"));
        }
    }
}
