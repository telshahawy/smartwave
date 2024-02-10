using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class allownullscolsinvisitactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                schema: "HomeVisits",
                table: "VisitActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                schema: "HomeVisits",
                table: "VisitActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitActions_ReasonId",
                schema: "HomeVisits",
                table: "VisitActions",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitActions_Reasons_ReasonId",
                schema: "HomeVisits",
                table: "VisitActions",
                column: "ReasonId",
                principalSchema: "HomeVisits",
                principalTable: "Reasons",
                principalColumn: "ReasonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitActions_Reasons_ReasonId",
                schema: "HomeVisits",
                table: "VisitActions");

            migrationBuilder.DropIndex(
                name: "IX_VisitActions_ReasonId",
                schema: "HomeVisits",
                table: "VisitActions");

            migrationBuilder.DropColumn(
                name: "Comments",
                schema: "HomeVisits",
                table: "VisitActions");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                schema: "HomeVisits",
                table: "VisitActions");
        }
    }
}
