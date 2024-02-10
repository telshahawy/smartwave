using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddClientRelationToPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_ClientId",
                schema: "HomeVisits",
                table: "Patients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Clients_ClientId",
                schema: "HomeVisits",
                table: "Patients",
                column: "ClientId",
                principalSchema: "HomeVisits",
                principalTable: "Clients",
                principalColumn: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Clients_ClientId",
                schema: "HomeVisits",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ClientId",
                schema: "HomeVisits",
                table: "Patients");
        }
    }
}
