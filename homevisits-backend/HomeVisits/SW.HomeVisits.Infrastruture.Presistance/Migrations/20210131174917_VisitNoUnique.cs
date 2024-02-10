using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class VisitNoUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitNo",
                schema: "HomeVisits",
                table: "Visits",
                column: "VisitNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visits_VisitNo",
                schema: "HomeVisits",
                table: "Visits");
        }
    }
}
