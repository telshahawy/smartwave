using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class UpdateVisitStatusCreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChemistVisitOrder_Chemists_ChemistId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChemistVisitOrder_TimeZoneFrames_TimeZoneFrameId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChemistVisitOrder_Visits_VisitId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "HomeVisits",
                table: "VisitStatus",
                nullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChemistVisitOrder_Chemists_ChemistId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder",
            //    column: "ChemistId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "Chemists",
            //    principalColumn: "ChemistId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChemistVisitOrder_TimeZoneFrames_TimeZoneFrameId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder",
            //    column: "TimeZoneFrameId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "TimeZoneFrames",
            //    principalColumn: "TimeZoneFrameId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChemistVisitOrder_Visits_VisitId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder",
            //    column: "VisitId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "Visits",
            //    principalColumn: "VisitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChemistVisitOrder_Chemists_ChemistId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChemistVisitOrder_TimeZoneFrames_TimeZoneFrameId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChemistVisitOrder_Visits_VisitId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "HomeVisits",
                table: "VisitStatus");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChemistVisitOrder_Chemists_ChemistId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder",
            //    column: "ChemistId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "Chemists",
            //    principalColumn: "ChemistId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChemistVisitOrder_TimeZoneFrames_TimeZoneFrameId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder",
            //    column: "TimeZoneFrameId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "TimeZoneFrames",
            //    principalColumn: "TimeZoneFrameId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChemistVisitOrder_Visits_VisitId",
            //    schema: "HomeVisits",
            //    table: "ChemistVisitOrder",
            //    column: "VisitId",
            //    principalSchema: "HomeVisits",
            //    principalTable: "Visits",
            //    principalColumn: "VisitId",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
