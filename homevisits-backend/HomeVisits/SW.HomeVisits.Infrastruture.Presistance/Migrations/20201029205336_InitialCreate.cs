using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HomeVisits");

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "HomeVisits",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Reciever = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Message = table.Column<string>(maxLength: 500, nullable: false),
                    Link = table.Column<string>(maxLength: 250, nullable: true),
                    NotificationType = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "HomeVisits",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(nullable: false),
                    PatientNo = table.Column<string>(maxLength: 100, nullable: false),
                    DOB = table.Column<string>(maxLength: 250, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "ReasonActions",
                schema: "HomeVisits",
                columns: table => new
                {
                    ReasonActionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonActions", x => x.ReasonActionId);
                });

            migrationBuilder.CreateTable(
                name: "SystemPages",
                schema: "HomeVisits",
                columns: table => new
                {
                    SystemPageId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(maxLength: 250, nullable: false),
                    Position = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPages", x => x.SystemPageId);
                });

            migrationBuilder.CreateTable(
                name: "VisitActionType",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitActionTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionNameEn = table.Column<string>(maxLength: 100, nullable: false),
                    ActionNameAr = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitActionType", x => x.VisitActionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VisitStatusTypes",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitStatusTypeId = table.Column<int>(nullable: false),
                    StatusNameEn = table.Column<string>(maxLength: 100, nullable: false),
                    StatusNameAr = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitStatusTypes", x => x.VisitStatusTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VisitType",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitTypeId = table.Column<int>(nullable: false),
                    TypeNameEn = table.Column<string>(maxLength: 100, nullable: false),
                    TypeNameAr = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitType", x => x.VisitTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PatientPhones",
                schema: "HomeVisits",
                columns: table => new
                {
                    PatientPhoneId = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPhones", x => x.PatientPhoneId);
                    table.ForeignKey(
                        name: "FK_PatientPhones_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "HomeVisits",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(maxLength: 250, nullable: false),
                    Position = table.Column<int>(nullable: true),
                    SystemPageId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_SystemPages_SystemPageId",
                        column: x => x.SystemPageId,
                        principalSchema: "HomeVisits",
                        principalTable: "SystemPages",
                        principalColumn: "SystemPageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionUsages",
                schema: "HomeVisits",
                columns: table => new
                {
                    PermissionUsageId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    Usage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionUsages", x => x.PermissionUsageId);
                    table.ForeignKey(
                        name: "FK_PermissionUsages_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "HomeVisits",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId");
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitId = table.Column<Guid>(nullable: false),
                    VisitNo = table.Column<string>(maxLength: 50, nullable: false),
                    VisitTypeId = table.Column<int>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    PatientId = table.Column<Guid>(nullable: false),
                    PatientAddressId = table.Column<Guid>(nullable: false),
                    ChemistId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    RelativeAgeSegmentId = table.Column<Guid>(nullable: true),
                    TimeZoneGeoZoneId = table.Column<Guid>(nullable: false),
                    PlannedNoOfPatients = table.Column<int>(nullable: false),
                    RequiredTests = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    OriginVisitId = table.Column<Guid>(nullable: true),
                    MinMinutes = table.Column<int>(nullable: true),
                    MaxMinutes = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visits_Visits_OriginVisitId",
                        column: x => x.OriginVisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_Visits_VisitType_VisitTypeId",
                        column: x => x.VisitTypeId,
                        principalSchema: "HomeVisits",
                        principalTable: "VisitType",
                        principalColumn: "VisitTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "HomeVisits",
                columns: table => new
                {
                    AttachmentId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(maxLength: 500, nullable: false),
                    VisitId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachments_Visits_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LostVisitTime",
                schema: "HomeVisits",
                columns: table => new
                {
                    LostVisitTimeId = table.Column<Guid>(nullable: false),
                    VisitId = table.Column<Guid>(nullable: false),
                    LostTime = table.Column<TimeSpan>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostVisitTime", x => x.LostVisitTimeId);
                    table.ForeignKey(
                        name: "FK_LostVisitTime_Visits_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId");
                });

            migrationBuilder.CreateTable(
                name: "VisitActions",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitActionId = table.Column<Guid>(nullable: false),
                    VisitId = table.Column<Guid>(nullable: false),
                    VisitActionTypeId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitActions", x => x.VisitActionId);
                    table.ForeignKey(
                        name: "FK_VisitActions_VisitActionType_VisitActionTypeId",
                        column: x => x.VisitActionTypeId,
                        principalSchema: "HomeVisits",
                        principalTable: "VisitActionType",
                        principalColumn: "VisitActionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitActions_Visits_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitNotifications",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitNotificationId = table.Column<Guid>(nullable: false),
                    VisitId = table.Column<Guid>(nullable: false),
                    NotificationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitNotifications", x => x.VisitNotificationId);
                    table.ForeignKey(
                        name: "FK_VisitNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalSchema: "HomeVisits",
                        principalTable: "Notifications",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitNotifications_Visits_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChemistSchedule",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistScheduleId = table.Column<Guid>(nullable: false),
                    ChemistAssignedGeoZoneId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartLatitude = table.Column<float>(nullable: false),
                    StartLangitude = table.Column<float>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistSchedule", x => x.ChemistScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "ChemistScheduleDays",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistScheduleDayId = table.Column<Guid>(nullable: false),
                    ChemistScheduleId = table.Column<Guid>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistScheduleDays", x => x.ChemistScheduleDayId);
                    table.ForeignKey(
                        name: "FK_ChemistScheduleDays_ChemistSchedule_ChemistScheduleId",
                        column: x => x.ChemistScheduleId,
                        principalSchema: "HomeVisits",
                        principalTable: "ChemistSchedule",
                        principalColumn: "ChemistScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChemistAssignedGeoZones",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistAssignedGeoZoneId = table.Column<Guid>(nullable: false),
                    ChemistId = table.Column<Guid>(nullable: false),
                    GeoZoneId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistAssignedGeoZones", x => x.ChemistAssignedGeoZoneId);
                });

            migrationBuilder.CreateTable(
                name: "ChemistTrackingLog",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistTrackingLogId = table.Column<Guid>(nullable: false),
                    ChemistId = table.Column<Guid>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    DeviceSerialNumber = table.Column<string>(maxLength: 100, nullable: false),
                    MobileBatteryPercentage = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 250, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistTrackingLog", x => x.ChemistTrackingLogId);
                });

            migrationBuilder.CreateTable(
                name: "OnHoldVisits",
                schema: "HomeVisits",
                columns: table => new
                {
                    OnHoldVisitId = table.Column<Guid>(nullable: false),
                    ChemistId = table.Column<Guid>(nullable: false),
                    DeviceSerialNo = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    TimeZoneFrameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnHoldVisits", x => x.OnHoldVisitId);
                });

            migrationBuilder.CreateTable(
                name: "AgeSegments",
                schema: "HomeVisits",
                columns: table => new
                {
                    AgeSegmentId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    AgeFromDay = table.Column<int>(nullable: false),
                    AgeFromMonth = table.Column<int>(nullable: false),
                    AgeFromYear = table.Column<int>(nullable: false),
                    AgeToDay = table.Column<int>(nullable: false),
                    AgeToMonth = table.Column<int>(nullable: false),
                    AgeToYear = table.Column<int>(nullable: false),
                    AgeFromInclusive = table.Column<bool>(nullable: false),
                    AgeToInclusive = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    NeedExpert = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeSegments", x => x.AgeSegmentId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "HomeVisits",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    CountryNameEn = table.Column<string>(maxLength: 100, nullable: false),
                    CountryNameAr = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "HomeVisits",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(nullable: false),
                    ClientCode = table.Column<string>(maxLength: 50, nullable: false),
                    ClientName = table.Column<string>(maxLength: 250, nullable: false),
                    CountryId = table.Column<Guid>(nullable: false),
                    URLName = table.Column<string>(maxLength: 250, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Logo = table.Column<string>(maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "HomeVisits",
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Governats",
                schema: "HomeVisits",
                columns: table => new
                {
                    GovernateId = table.Column<Guid>(nullable: false),
                    GoverNameEn = table.Column<string>(maxLength: 100, nullable: false),
                    GoverNameAr = table.Column<string>(maxLength: 100, nullable: false),
                    CountryId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governats", x => x.GovernateId);
                    table.ForeignKey(
                        name: "FK_Governats_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "HomeVisits",
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reasons",
                schema: "HomeVisits",
                columns: table => new
                {
                    ReasonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    ReasonActionId = table.Column<int>(nullable: true),
                    VisitTypeActionId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.ReasonId);
                    table.ForeignKey(
                        name: "FK_Reasons_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reasons_ReasonActions_ReasonActionId",
                        column: x => x.ReasonActionId,
                        principalSchema: "HomeVisits",
                        principalTable: "ReasonActions",
                        principalColumn: "ReasonActionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reasons_VisitActionType_VisitTypeActionId",
                        column: x => x.VisitTypeActionId,
                        principalSchema: "HomeVisits",
                        principalTable: "VisitActionType",
                        principalColumn: "VisitActionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "HomeVisits",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(maxLength: 250, nullable: false),
                    NameEn = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "HomeVisits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    UserName = table.Column<string>(maxLength: 250, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 500, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 500, nullable: false),
                    SecurityStamp = table.Column<string>(maxLength: 500, nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    PersonalPhoto = table.Column<string>(nullable: true),
                    UserCreationTypes = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoZones",
                schema: "HomeVisits",
                columns: table => new
                {
                    GeoZoneId = table.Column<Guid>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(maxLength: 100, nullable: false),
                    KmlFilePath = table.Column<string>(maxLength: 1000, nullable: true),
                    MappingCode = table.Column<string>(nullable: true),
                    GovernateId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoZones", x => x.GeoZoneId);
                    table.ForeignKey(
                        name: "FK_GeoZones_Governats_GovernateId",
                        column: x => x.GovernateId,
                        principalSchema: "HomeVisits",
                        principalTable: "Governats",
                        principalColumn: "GovernateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitStatus",
                schema: "HomeVisits",
                columns: table => new
                {
                    VisitStatusId = table.Column<Guid>(nullable: false),
                    VisitId = table.Column<Guid>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    DeviceSerialNumber = table.Column<string>(maxLength: 100, nullable: false),
                    MobileBatteryPercentage = table.Column<int>(nullable: false),
                    VisitActionTypeId = table.Column<int>(nullable: false),
                    VisitStatusTypeId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ActualNoOfPatients = table.Column<int>(nullable: true),
                    NoOfTests = table.Column<int>(nullable: true),
                    IsAddressVerified = table.Column<bool>(nullable: true),
                    ReasonId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitStatus", x => x.VisitStatusId);
                    table.ForeignKey(
                        name: "FK_VisitStatus_Reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "HomeVisits",
                        principalTable: "Reasons",
                        principalColumn: "ReasonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitStatus_VisitActionType_VisitActionTypeId",
                        column: x => x.VisitActionTypeId,
                        principalSchema: "HomeVisits",
                        principalTable: "VisitActionType",
                        principalColumn: "VisitActionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitStatus_Visits_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitStatus_VisitStatusTypes_VisitStatusTypeId",
                        column: x => x.VisitStatusTypeId,
                        principalSchema: "HomeVisits",
                        principalTable: "VisitStatusTypes",
                        principalColumn: "VisitStatusTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "HomeVisits",
                columns: table => new
                {
                    RolePermissionId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolePermissionId);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "HomeVisits",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "HomeVisits",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chemists",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    DOB = table.Column<int>(nullable: false),
                    ExpertChemist = table.Column<bool>(nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemists", x => x.ChemistId);
                    table.ForeignKey(
                        name: "FK_Chemists_Users_ChemistId",
                        column: x => x.ChemistId,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "HomeVisits",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "HomeVisits",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAddress",
                schema: "HomeVisits",
                columns: table => new
                {
                    PatientAddressId = table.Column<Guid>(nullable: false),
                    street = table.Column<string>(maxLength: 250, nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    LocationUrl = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: false),
                    Flat = table.Column<string>(nullable: false),
                    Building = table.Column<string>(maxLength: 250, nullable: false),
                    PatientId = table.Column<Guid>(nullable: false),
                    AdditionalInfo = table.Column<string>(maxLength: 500, nullable: true),
                    GeoZoneId = table.Column<Guid>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ConfirmedBy = table.Column<Guid>(nullable: true),
                    ConfirmedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAddress", x => x.PatientAddressId);
                    table.ForeignKey(
                        name: "FK_PatientAddress_GeoZones_GeoZoneId",
                        column: x => x.GeoZoneId,
                        principalSchema: "HomeVisits",
                        principalTable: "GeoZones",
                        principalColumn: "GeoZoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAddress_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "TimeZoneFrames",
                schema: "HomeVisits",
                columns: table => new
                {
                    TimeZoneFrameId = table.Column<Guid>(nullable: false),
                    GeoZoneId = table.Column<Guid>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: false),
                    NameEN = table.Column<string>(maxLength: 100, nullable: false),
                    VisitsNoQouta = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BranchDispatch = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZoneFrames", x => x.TimeZoneFrameId);
                    table.ForeignKey(
                        name: "FK_TimeZoneFrames_GeoZones_GeoZoneId",
                        column: x => x.GeoZoneId,
                        principalSchema: "HomeVisits",
                        principalTable: "GeoZones",
                        principalColumn: "GeoZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgeSegments_ClientId",
                schema: "HomeVisits",
                table: "AgeSegments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_VisitId",
                schema: "HomeVisits",
                table: "Attachments",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistAssignedGeoZones_ChemistId",
                schema: "HomeVisits",
                table: "ChemistAssignedGeoZones",
                column: "ChemistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistAssignedGeoZones_GeoZoneId",
                schema: "HomeVisits",
                table: "ChemistAssignedGeoZones",
                column: "GeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistSchedule_ChemistAssignedGeoZoneId",
                schema: "HomeVisits",
                table: "ChemistSchedule",
                column: "ChemistAssignedGeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistScheduleDays_ChemistScheduleId",
                schema: "HomeVisits",
                table: "ChemistScheduleDays",
                column: "ChemistScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistTrackingLog_ChemistId",
                schema: "HomeVisits",
                table: "ChemistTrackingLog",
                column: "ChemistId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CountryId",
                schema: "HomeVisits",
                table: "Clients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ClientId",
                schema: "HomeVisits",
                table: "Countries",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GeoZones_GovernateId",
                schema: "HomeVisits",
                table: "GeoZones",
                column: "GovernateId");

            migrationBuilder.CreateIndex(
                name: "IX_Governats_CountryId",
                schema: "HomeVisits",
                table: "Governats",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LostVisitTime_VisitId",
                schema: "HomeVisits",
                table: "LostVisitTime",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_OnHoldVisits_ChemistId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                column: "ChemistId");

            migrationBuilder.CreateIndex(
                name: "IX_OnHoldVisits_TimeZoneFrameId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                column: "TimeZoneFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAddress_GeoZoneId",
                schema: "HomeVisits",
                table: "PatientAddress",
                column: "GeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAddress_PatientId",
                schema: "HomeVisits",
                table: "PatientAddress",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhones_PatientId",
                schema: "HomeVisits",
                table: "PatientPhones",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SystemPageId",
                schema: "HomeVisits",
                table: "Permissions",
                column: "SystemPageId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionUsages_PermissionId",
                schema: "HomeVisits",
                table: "PermissionUsages",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ClientId",
                schema: "HomeVisits",
                table: "Reasons",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_ReasonActionId",
                schema: "HomeVisits",
                table: "Reasons",
                column: "ReasonActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_VisitTypeActionId",
                schema: "HomeVisits",
                table: "Reasons",
                column: "VisitTypeActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "HomeVisits",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ClientId",
                schema: "HomeVisits",
                table: "Roles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeZoneFrames_GeoZoneId",
                schema: "HomeVisits",
                table: "TimeZoneFrames",
                column: "GeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "HomeVisits",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "HomeVisits",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientId",
                schema: "HomeVisits",
                table: "Users",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitActions_VisitActionTypeId",
                schema: "HomeVisits",
                table: "VisitActions",
                column: "VisitActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitActions_VisitId",
                schema: "HomeVisits",
                table: "VisitActions",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotifications_NotificationId",
                schema: "HomeVisits",
                table: "VisitNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitNotifications_VisitId",
                schema: "HomeVisits",
                table: "VisitNotifications",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ChemistId",
                schema: "HomeVisits",
                table: "Visits",
                column: "ChemistId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_OriginVisitId",
                schema: "HomeVisits",
                table: "Visits",
                column: "OriginVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientAddressId",
                schema: "HomeVisits",
                table: "Visits",
                column: "PatientAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientId",
                schema: "HomeVisits",
                table: "Visits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_RelativeAgeSegmentId",
                schema: "HomeVisits",
                table: "Visits",
                column: "RelativeAgeSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_TimeZoneGeoZoneId",
                schema: "HomeVisits",
                table: "Visits",
                column: "TimeZoneGeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitTypeId",
                schema: "HomeVisits",
                table: "Visits",
                column: "VisitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitStatus_ReasonId",
                schema: "HomeVisits",
                table: "VisitStatus",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitStatus_VisitActionTypeId",
                schema: "HomeVisits",
                table: "VisitStatus",
                column: "VisitActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitStatus_VisitId",
                schema: "HomeVisits",
                table: "VisitStatus",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitStatus_VisitStatusTypeId",
                schema: "HomeVisits",
                table: "VisitStatus",
                column: "VisitStatusTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Chemists_ChemistId",
                schema: "HomeVisits",
                table: "Visits",
                column: "ChemistId",
                principalSchema: "HomeVisits",
                principalTable: "Chemists",
                principalColumn: "ChemistId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_TimeZoneFrames_TimeZoneGeoZoneId",
                schema: "HomeVisits",
                table: "Visits",
                column: "TimeZoneGeoZoneId",
                principalSchema: "HomeVisits",
                principalTable: "TimeZoneFrames",
                principalColumn: "TimeZoneFrameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_PatientAddress_PatientAddressId",
                schema: "HomeVisits",
                table: "Visits",
                column: "PatientAddressId",
                principalSchema: "HomeVisits",
                principalTable: "PatientAddress",
                principalColumn: "PatientAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AgeSegments_RelativeAgeSegmentId",
                schema: "HomeVisits",
                table: "Visits",
                column: "RelativeAgeSegmentId",
                principalSchema: "HomeVisits",
                principalTable: "AgeSegments",
                principalColumn: "AgeSegmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChemistSchedule_ChemistAssignedGeoZones_ChemistAssignedGeoZoneId",
                schema: "HomeVisits",
                table: "ChemistSchedule",
                column: "ChemistAssignedGeoZoneId",
                principalSchema: "HomeVisits",
                principalTable: "ChemistAssignedGeoZones",
                principalColumn: "ChemistAssignedGeoZoneId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChemistAssignedGeoZones_Chemists_ChemistId",
                schema: "HomeVisits",
                table: "ChemistAssignedGeoZones",
                column: "ChemistId",
                principalSchema: "HomeVisits",
                principalTable: "Chemists",
                principalColumn: "ChemistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChemistAssignedGeoZones_GeoZones_GeoZoneId",
                schema: "HomeVisits",
                table: "ChemistAssignedGeoZones",
                column: "GeoZoneId",
                principalSchema: "HomeVisits",
                principalTable: "GeoZones",
                principalColumn: "GeoZoneId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChemistTrackingLog_Chemists_ChemistId",
                schema: "HomeVisits",
                table: "ChemistTrackingLog",
                column: "ChemistId",
                principalSchema: "HomeVisits",
                principalTable: "Chemists",
                principalColumn: "ChemistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnHoldVisits_Chemists_ChemistId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                column: "ChemistId",
                principalSchema: "HomeVisits",
                principalTable: "Chemists",
                principalColumn: "ChemistId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnHoldVisits_TimeZoneFrames_TimeZoneFrameId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                column: "TimeZoneFrameId",
                principalSchema: "HomeVisits",
                principalTable: "TimeZoneFrames",
                principalColumn: "TimeZoneFrameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgeSegments_Clients_ClientId",
                schema: "HomeVisits",
                table: "AgeSegments",
                column: "ClientId",
                principalSchema: "HomeVisits",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Clients_ClientId",
                schema: "HomeVisits",
                table: "Countries",
                column: "ClientId",
                principalSchema: "HomeVisits",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Clients_ClientId",
                schema: "HomeVisits",
                table: "Countries");

            migrationBuilder.DropTable(
                name: "Attachments",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "ChemistScheduleDays",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "ChemistTrackingLog",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "LostVisitTime",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "OnHoldVisits",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "PatientPhones",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "PermissionUsages",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "VisitActions",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "VisitNotifications",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "VisitStatus",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "ChemistSchedule",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Reasons",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Visits",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "VisitStatusTypes",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "ChemistAssignedGeoZones",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "SystemPages",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "ReasonActions",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "VisitActionType",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "PatientAddress",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "AgeSegments",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "TimeZoneFrames",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "VisitType",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Chemists",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "GeoZones",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Governats",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "HomeVisits");
        }
    }
}
