using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class addcolstogovernatestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                schema: "HomeVisits",
                table: "Governats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerServiceEmail",
                schema: "HomeVisits",
                table: "Governats",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "HomeVisits",
                table: "Governats");

            migrationBuilder.DropColumn(
                name: "CustomerServiceEmail",
                schema: "HomeVisits",
                table: "Governats");
        }
    }
}
