using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddIsDeletedRoleGeoZones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "HomeVisits",
                table: "RolePermissions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "HomeVisits",
                table: "RoleGeoZones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "HomeVisits",
                table: "RoleGeoZones");
        }
    }
}
