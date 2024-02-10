using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddVisitCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitCode",
                schema: "HomeVisits",
                table: "Visits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitCode",
                schema: "HomeVisits",
                table: "Visits",
                column: "VisitCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visits_VisitCode",
                schema: "HomeVisits",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitCode",
                schema: "HomeVisits",
                table: "Visits");
        }
    }
}
