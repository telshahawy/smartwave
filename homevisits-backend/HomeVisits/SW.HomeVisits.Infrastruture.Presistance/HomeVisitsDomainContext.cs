using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Application;
using SW.HomeVisits.Application.Abstract;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Infrastruture.Presistance;
using SW.HomeVisits.Infrastruture.Presistance.Configuration;
using SW.HomeVisits.Infrastruture.Presistance.Extentions;

namespace SW.HomeVisits.Infrastruture.Data
{
    public class HomeVisitsDomainContext : DataContext<HomeVisitsDomainContext>
    {
        //private readonly IServiceProvider _serviceProvider;
        public HomeVisitsDomainContext(DbContextOptions<HomeVisitsDomainContext> options)
            : base(options)
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.;Initial Catalog=HomeVisitsDBUAT2;User ID=smart;Password=123456",
        //: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDB_CR;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#",
        //: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.;Initial Catalog=HomeVisitsDB_CR",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBStaging;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBTest;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBLive;User ID=hvdblive;Password=CzHlzXGa97T302t",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog = HomeVisitsDBUAT;User ID=hvdbuat;Password=up7zYatHtEX2NH4",

        //    builder =>
        //        {
        //            builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //        }))
        {
            //_serviceProvider = serviceProvider;
            //this.Database.EnsureCreated();
        }

        public DbSet<SystemParameter> SystemParameters { get; set; }
        public DbSet<AgeSegment> AgeSegments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Chemist> Chemists { get; set; }
        public DbSet<ChemistAssignedGeoZone> ChemistAssignedGeoZones { get; set; }
        public DbSet<ChemistSchedule> ChemistSchedules { get; set; }
        public DbSet<ChemistScheduleDay> ChemistScheduleDays { get; set; }
        public DbSet<ChemistTrackingLog> ChemistTrackingLogs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<GeoZone> GeoZones { get; set; }
        public DbSet<Governate> Governates { get; set; }
        public DbSet<LostVisitTime> LostVisitTimes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OnHoldVisit> OnHoldVisits { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddress { get; set; }
        public DbSet<PatientPhone> PatientPhones { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionUsage> PermissionUsages { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<ReasonAction> ReasonActions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SystemPage> SystemPages { get; set; }
        public DbSet<TimeZoneFrame> timeZoneFrame { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitAction> VisitActions { get; set; }
        public DbSet<VisitActionType> VisitActionTyped { get; set; }
        public DbSet<VisitNotification> VisitNotifications { get; set; }
        public DbSet<VisitStatus> VisitStatus { get; set; }
        public DbSet<VisitStatusType> VisitStatusTypes { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<RoleGeoZone> RoleGeoZones { get; set; }
        public DbSet<UserGeoZone> UserGeoZones { get; set; }
        public DbSet<ChemistPermit> ChemistPermits { get; set; }
        public DbSet<ChemistVisitOrder> ChemistVisitOrder { get; set; }
        public DbSet<UserAdditionalPermission> UserAdditionalPermissions { get; set; }
        public DbSet<UserExcludedRolePermission> UserExcludedRolePermissions { get; set; }
        public DbSet<SystemPagePermission> SystemPagePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HomeVisits");
            modelBuilder.ApplyConfiguration(new AgeSegmentConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistAssignedGeoZoneConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistScheduleDayConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistTrackingLogConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new GeoZoneConfiguration());
            modelBuilder.ApplyConfiguration(new GovernateConfiguration());
            modelBuilder.ApplyConfiguration(new LostVisitTimeConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new OnHoldVisitConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new PatientAddressConfiguration());
            modelBuilder.ApplyConfiguration(new PatientPhoneConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionUsageConfiguration());
            modelBuilder.ApplyConfiguration(new ReasonConfiguration());
            modelBuilder.ApplyConfiguration(new ReasonActionConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new SystemPageConfiguration());
            modelBuilder.ApplyConfiguration(new SystemParametersConfiguration());
            modelBuilder.ApplyConfiguration(new TimeZoneFrameConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VisitConfiguration());
            modelBuilder.ApplyConfiguration(new VisitActionConfiguration());
            modelBuilder.ApplyConfiguration(new VisitActionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VisitNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new VisitStatusConfiguration());
            modelBuilder.ApplyConfiguration(new VisitStatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VisitTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new RoleGeoZoneConfiguration());
            modelBuilder.ApplyConfiguration(new UserGeoZoneConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistPermitConfiguration());
            modelBuilder.ApplyConfiguration(new ChemistVisitOrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserAdditionalPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserExcludedRolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new SystemPagePermissionConfiguration());
            //modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}