using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class RoleIdToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                schema: "HomeVisits",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                schema: "HomeVisits",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserGeoZones",
                schema: "HomeVisits",
                columns: table => new
                {
                    UserGeoZoneId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GeoZoneId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGeoZones", x => x.UserGeoZoneId);
                    table.ForeignKey(
                        name: "FK_UserGeoZones_GeoZones_GeoZoneId",
                        column: x => x.GeoZoneId,
                        principalSchema: "HomeVisits",
                        principalTable: "GeoZones",
                        principalColumn: "GeoZoneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGeoZones_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "HomeVisits",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGeoZones_GeoZoneId",
                schema: "HomeVisits",
                table: "UserGeoZones",
                column: "GeoZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGeoZones_UserId",
                schema: "HomeVisits",
                table: "UserGeoZones",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "HomeVisits",
                table: "Users",
                column: "RoleId",
                principalSchema: "HomeVisits",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "HomeVisits",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserGeoZones",
                schema: "HomeVisits");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                schema: "HomeVisits",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "HomeVisits",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                schema: "HomeVisits",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
