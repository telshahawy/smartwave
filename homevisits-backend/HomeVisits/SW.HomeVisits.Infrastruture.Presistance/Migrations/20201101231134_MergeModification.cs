using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class MergeModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageAr",
                schema: "HomeVisits",
                table: "Notifications",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                schema: "HomeVisits",
                table: "Notifications",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageAr",
                schema: "HomeVisits",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                schema: "HomeVisits",
                table: "Notifications");
        }
    }
}
