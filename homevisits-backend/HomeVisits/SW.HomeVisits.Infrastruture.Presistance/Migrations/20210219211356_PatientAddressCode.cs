using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class PatientAddressCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                schema: "HomeVisits",
                table: "PatientAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientAddress_Code",
                schema: "HomeVisits",
                table: "PatientAddress",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PatientAddress_Code",
                schema: "HomeVisits",
                table: "PatientAddress");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "HomeVisits",
                table: "PatientAddress");
        }
    }
}
