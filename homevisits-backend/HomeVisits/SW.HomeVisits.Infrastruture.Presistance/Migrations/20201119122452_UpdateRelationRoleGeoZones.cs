using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class UpdateRelationRoleGeoZones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleGeoZones_Roles_RoleId",
                schema: "HomeVisits",
                table: "RoleGeoZones");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGeoZones_Roles_RoleId",
                schema: "HomeVisits",
                table: "RoleGeoZones",
                column: "RoleId",
                principalSchema: "HomeVisits",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleGeoZones_Roles_RoleId",
                schema: "HomeVisits",
                table: "RoleGeoZones");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleGeoZones_Roles_RoleId",
                schema: "HomeVisits",
                table: "RoleGeoZones",
                column: "RoleId",
                principalSchema: "HomeVisits",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
