using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class addChemistVisitOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OnHoldVisits_Chemists_ChemistId",
            //    schema: "HomeVisits",
            //    table: "OnHoldVisits");

            migrationBuilder.DropIndex(
                name: "IX_OnHoldVisits_ChemistId",
                schema: "HomeVisits",
                table: "OnHoldVisits");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceSerialNo",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChemistId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "ChemistVisitOrder",
                schema: "HomeVisits",
                columns: table => new
                {
                    ChemistVisitOrderId = table.Column<Guid>(nullable: false),
                    VisitId = table.Column<Guid>(nullable: false),
                    ChemistId = table.Column<Guid>(nullable: false),
                    TimeZoneFrameId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VisitOrder = table.Column<int>(nullable: false),
                    StartLatitude = table.Column<float>(nullable: false),
                    StartLangitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemistVisitOrder", x => x.ChemistVisitOrderId);
                    table.ForeignKey(
                        name: "FK_ChemistVisitOrder_Chemists_ChemistId",
                        column: x => x.ChemistId,
                        principalSchema: "HomeVisits",
                        principalTable: "Chemists",
                        principalColumn: "ChemistId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChemistVisitOrder_TimeZoneFrames_TimeZoneFrameId",
                        column: x => x.TimeZoneFrameId,
                        principalSchema: "HomeVisits",
                        principalTable: "TimeZoneFrames",
                        principalColumn: "TimeZoneFrameId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChemistVisitOrder_Visits_VisitId",
                        column: x => x.VisitId,
                        principalSchema: "HomeVisits",
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChemistVisitOrder_ChemistId",
                schema: "HomeVisits",
                table: "ChemistVisitOrder",
                column: "ChemistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistVisitOrder_TimeZoneFrameId",
                schema: "HomeVisits",
                table: "ChemistVisitOrder",
                column: "TimeZoneFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_ChemistVisitOrder_VisitId",
                schema: "HomeVisits",
                table: "ChemistVisitOrder",
                column: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChemistVisitOrder",
                schema: "HomeVisits");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceSerialNo",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChemistId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnHoldVisits_ChemistId",
                schema: "HomeVisits",
                table: "OnHoldVisits",
                column: "ChemistId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OnHoldVisits_Chemists_ChemistId",
            //    schema: "HomeVisits",
            //    table: "OnHoldVisits",
            //    column: "ChemistId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "Chemists",
            //    principalColumn: "ChemistId");
        }
    }
}
