using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddRoleGeoZones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultPageId",
                schema: "HomeVisits",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoleGeoZones",
                schema: "HomeVisits",
                columns: table => new
                {
                    RoleGeoZoneId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    GeoZoneId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGeoZones", x => x.RoleGeoZoneId);
                    table.ForeignKey(
                        name: "FK_RoleGeoZones_GeoZones_GeoZoneId",
                        column: x => x.GeoZoneId,
                        principalSchema: "HomeVisits",
                        principalTable: "GeoZones",
                        principalColumn: "GeoZoneId");
                    table.ForeignKey(
                        name: "FK_RoleGeoZones_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "HomeVisits",
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DefaultPageId",
                schema: "HomeVisits",
                table: "Roles",
                column: "DefaultPageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeoZones_GeoZoneId",
                schema: "HomeVisits",
                table: "RoleGeoZones",
                column: "GeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGeoZones_RoleId",
                schema: "HomeVisits",
                table: "RoleGeoZones",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_SystemPages_DefaultPageId",
                schema: "HomeVisits",
                table: "Roles",
                column: "DefaultPageId",
                principalSchema: "HomeVisits",
                principalTable: "SystemPages",
                principalColumn: "SystemPageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_SystemPages_DefaultPageId",
                schema: "HomeVisits",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "RoleGeoZones",
                schema: "HomeVisits");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DefaultPageId",
                schema: "HomeVisits",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DefaultPageId",
                schema: "HomeVisits",
                table: "Roles");
        }
    }
}
