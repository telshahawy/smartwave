using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddSystemParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemParameters",
                schema: "HomeVisits",
                columns: table => new
                {
                    SystemParametersId = table.Column<Guid>(nullable: false),
                    EstimatedVisitDurationInMin = table.Column<int>(nullable: false),
                    NextReserveHomevisitInDay = table.Column<int>(nullable: false),
                    RoutingSlotDurationInMin = table.Column<int>(nullable: false),
                    VisitApprovalBy = table.Column<string>(nullable: true),
                    VisitCancelBy = table.Column<string>(nullable: true),
                    DefaultCountryId = table.Column<Guid>(nullable: true),
                    DefaultGovernorateId = table.Column<Guid>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: false),
                    IsSendPatientTimeConfirmation = table.Column<bool>(nullable: true),
                    IsOptimizezonebefore = table.Column<bool>(nullable: true),
                    OptimizezonebeforeInMin = table.Column<int>(nullable: true),
                    CallCenterNumber = table.Column<string>(nullable: true),
                    WhatsappBusinessLink = table.Column<string>(nullable: true),
                    PrecautionsFile = table.Column<string>(nullable: true),
                    CreateBy = table.Column<Guid>(nullable: false),
                    CountryId = table.Column<Guid>(nullable: true),
                    GovernateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemParameters", x => x.SystemParametersId);
                    table.ForeignKey(
                        name: "FK_SystemParameters_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "HomeVisits",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemParameters_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "HomeVisits",
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemParameters_Governats_GovernateId",
                        column: x => x.GovernateId,
                        principalSchema: "HomeVisits",
                        principalTable: "Governats",
                        principalColumn: "GovernateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_ClientId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_CountryId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "GovernateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemParameters",
                schema: "HomeVisits");
        }
    }
}
