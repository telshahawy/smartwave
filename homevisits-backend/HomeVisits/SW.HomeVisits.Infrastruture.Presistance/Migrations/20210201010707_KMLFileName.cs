using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class KMLFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KmlFileName",
                schema: "HomeVisits",
                table: "GeoZones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KmlFileName",
                schema: "HomeVisits",
                table: "GeoZones");
        }
    }
}
