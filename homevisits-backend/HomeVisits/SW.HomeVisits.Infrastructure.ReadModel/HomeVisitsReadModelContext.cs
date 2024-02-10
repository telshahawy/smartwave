using System;
using Microsoft.EntityFrameworkCore;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Application;
using SW.HomeVisits.Application.Abstract;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;

namespace SW.HomeVisits.Infrastructure.ReadModel
{
    public class HomeVisitsReadModelContext : DataContext<HomeVisitsReadModelContext>
    {
        public HomeVisitsReadModelContext(DbContextOptions<HomeVisitsReadModelContext> options, IHomeVisitsConfigurationProvider configurationProvider)
            : base(options)
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.;Initial Catalog=HomeVisitsDBUAT2;User ID=smart;Password=123456",
        //: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDB_CR;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBStaging;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=68.183.73.80;Initial Catalog=HomeVisitsDBTest;User ID=HomeVisitsUser;Password=SwHv2147@VhsW#",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBLive;User ID=hvdblive;Password=CzHlzXGa97T302t",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBLive;User ID=hvdblive;Password=CzHlzXGa97T302t",
        ////: base(new DbContextOptionsBuilder().UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=HomeVisitsDBUAT;User ID=hvdbuat;Password=up7zYatHtEX2NH4",

        //builder =>
        //   {
        //       builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

        //   }))
        {

            //this.Database.EnsureCreated();

        }

        public DbQuery<VisitDetailsReasonReportView> visitDetailsReasonReportViews { get; set; }
        public DbQuery<VisitsNotificationsPatientView> visitsNotificationsPatientViews { get; set; }
        public DbQuery<GetPatientsListView> GetPatientsListViews { get; set; }
        public DbQuery<SystemParametersView> SystemParametersViews { get; set; }
        public DbQuery<AllChemistHomePageView> AllChemistHomePageViews { get; set; }
        public DbQuery<UserAreasView> UserAreasViews { get; set; }
        public DbQuery<VisitsHomePageView> VisitsHomePageViews { get; set; }
        public DbQuery<ChemistScheduleHomePageView> ChemistScheduleHomePageViews { get; set; }
        public DbQuery<CityView> CityViews { get; set; }
        public DbQuery<ChemistScheduleView> ChemistScheduleViews { get; set; }
        public DbQuery<ChemistsView> ChemistsViews { get; set; }
        public DbQuery<VisitsNotificationsView> VisitsNotificationsViews { get; set; }
        public DbQuery<CountryView> CountryViews { get; set; }
        public DbQuery<GovernateView> GovernateViews { get; set; }
        public DbQuery<GeoZoneView> GeoZoneView { get; set; }
        public DbQuery<PatientSearchView> PatientSearchViews { get; set; }
        public DbQuery<PatientVisitsView> PatientVisitsViews { get; set; }
        public DbQuery<VisitDetailsView> VisitDetailsViews { get; set; }
        public DbQuery<VisitAttachmentView> VisitAttachmentViews { get; set; }
        public DbQuery<PatientPhoneNumbersView> PatientPhoneNumbersViews { get; set; }
        public DbQuery<PatientAddressView> PatientAddressViews { get; set; }

        public DbQuery<ChemistTimeZoneAvailabilityView> ChemistTimeZoneAvailabilityViews { get; set; }
        public DbQuery<GetAllVisitsView> GetAllVisitsView { get; set; }
        public DbQuery<ChemistAssignedGeoZonesView> ChemistAssignedGeoZonesViews { get; set; }

        public DbQuery<LostVisitTimesView> LostVisitTimeViews { get; set; }
        public DbQuery<ReasonsView> ReasonsViews { get; set; }
        public DbQuery<OnHoldVisitsView> OnHoldVisitsViews { get; set; }
        public DbQuery<UserDevicesView> UserDevicesViews { get; set; }
        public DbQuery<AgeSegmentsView> AgeSegmentsViews { get; set; }
        public DbQuery<ChemistSchedulePlan> ChemistSchedulePlanViews { get; set; }
        public DbQuery<ChemistTrackingLogView> ChemistTrackingLogViews { get; set; }
        public DbQuery<VisitsView> VisitsViews { get; set; }
        public DbQuery<VisitStatusView> VisitStatusViews { get; set; }
        public DbQuery<RolesView> RolesViews { get; set; }
        public DbQuery<RolesPermissionView> RolesPermissionViews { get; set; }
        public DbQuery<RolesGeoZonesView> RolesGeoZonesViews { get; set; }
        public DbQuery<UserView> UserViews { get; set; }
        public DbQuery<UserGeoZonesView> UserGeoZonesViews { get; set; }
        public DbQuery<SystemPagesWithPermissionsView> SystemPagesWithPermissionsViews { get; set; }
        public DbQuery<ClientView> ClientViews { get; set; }
        public DbQuery<PatientsListView> PatientsListViews { get; set; }
        public DbQuery<ChemistPermitsView> ChemistPermitsViews { get; set; }
        public DbQuery<TimeZoneFramesView> TimeZoneFramesViews { get; set; }
        public DbQuery<ChemistVisitsOrderView> ChemistVisitsOrderViews { get; set; }
        public DbQuery<UserPermissionView> UserPermissionViews { get; set; }
        public DbQuery<PermissionView> PermissionViews { get; set; }

        public DbQuery<ChemistsLastTrackingLogView> ChemistsLastTrackingLogViews { get; set; }
        public DbQuery<ChemistVisitsScheduleView> ChemistVisitsScheduleView { get; set; }
        public DbQuery<SystemPagesPermissionsView> SystemPagesPermissionsView { get; set; }
        public DbQuery<SystemPagesView> SystemPagesViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("HomeVisits");
            modelBuilder.Query<SystemParametersView>();
            modelBuilder.Query<CityView>();
            modelBuilder.Query<ChemistScheduleView>();
            modelBuilder.Query<ChemistsView>();
            modelBuilder.Query<VisitsNotificationsView>();
            modelBuilder.Query<CountryView>();
            modelBuilder.Query<GovernateView>();
            modelBuilder.Query<GeoZoneView>();
            modelBuilder.Query<PatientSearchView>();
            modelBuilder.Query<PatientVisitsView>();
            modelBuilder.Query<VisitDetailsView>();
            modelBuilder.Query<VisitAttachmentView>();
            modelBuilder.Query<PatientPhoneNumbersView>();
            modelBuilder.Query<PatientAddressView>();
            modelBuilder.Query<ChemistTimeZoneAvailabilityView>();
            modelBuilder.Query<GetAllVisitsView>();
            modelBuilder.Query<ChemistAssignedGeoZonesView>();
            modelBuilder.Query<LostVisitTimesView>();
            modelBuilder.Query<ReasonsView>();
            modelBuilder.Query<OnHoldVisitsView>();
            modelBuilder.Query<UserDevicesView>();
            modelBuilder.Query<AgeSegmentsView>();
            modelBuilder.Query<ChemistSchedulePlan>();
            modelBuilder.Query<ChemistTrackingLogView>();
            modelBuilder.Query<VisitsView>();
            modelBuilder.Query<ChemistVisitsScheduleView>();
            modelBuilder.Query<VisitStatusView>();
            modelBuilder.Query<RolesView>();
            modelBuilder.Query<RolesPermissionView>();
            modelBuilder.Query<RolesGeoZonesView>();
            modelBuilder.Query<UserView>();
            modelBuilder.Query<UserGeoZonesView>();
            modelBuilder.Query<SystemPagesWithPermissionsView>();
            modelBuilder.Query<ClientView>();
            modelBuilder.Query<PatientsListView>();
            modelBuilder.Query<ChemistPermitsView>();
            modelBuilder.Query<TimeZoneFramesView>();
            modelBuilder.Query<UserPermissionView>();
            modelBuilder.Query<ChemistsLastTrackingLogView>();

        }
    }
}