using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddDurationInVisitOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Distance",
                schema: "HomeVisits",
                table: "ChemistVisitOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                schema: "HomeVisits",
                table: "ChemistVisitOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationInTraffic",
                schema: "HomeVisits",
                table: "ChemistVisitOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                schema: "HomeVisits",
                table: "ChemistVisitOrder");

            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "HomeVisits",
                table: "ChemistVisitOrder");

            migrationBuilder.DropColumn(
                name: "DurationInTraffic",
                schema: "HomeVisits",
                table: "ChemistVisitOrder");
        }
    }
}
