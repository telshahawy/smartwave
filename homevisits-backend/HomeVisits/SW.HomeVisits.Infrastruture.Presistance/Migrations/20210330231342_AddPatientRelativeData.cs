using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddPatientRelativeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelativeGender",
                schema: "HomeVisits",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativeName",
                schema: "HomeVisits",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativePhoneNumber",
                schema: "HomeVisits",
                table: "Visits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelativeGender",
                schema: "HomeVisits",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "RelativeName",
                schema: "HomeVisits",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "RelativePhoneNumber",
                schema: "HomeVisits",
                table: "Visits");
        }
    }
}
