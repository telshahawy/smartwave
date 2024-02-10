using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddChemistPermits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChemistPermits",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistPermitId = table.Column<Guid>(nullable: false),
                    ChemistId = table.Column<Guid>(nullable: false),
                    PermitDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistPermits", x => x.ChemistPermitId);
                    table.ForeignKey(
                        name: "FK_ChemistPermits_Chemists_ChemistId",
                        column: x => x.ChemistId,
                        principalSchema: "HomeVisits",
                        principalTable: "Chemists",
                        principalColumn: "ChemistId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChemistPermits_ChemistId",
                schema: "HomeVisits",
                table: "ChemistPermits",
                column: "ChemistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChemistPermits",
                schema: "HomeVisits");
        }
    }
}
