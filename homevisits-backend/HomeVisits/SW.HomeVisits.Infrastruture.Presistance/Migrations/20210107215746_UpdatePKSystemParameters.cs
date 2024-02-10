using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class UpdatePKSystemParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemParameters_Countries_CountryId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemParameters_Governats_GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemParameters",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropIndex(
                name: "IX_SystemParameters_ClientId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropIndex(
                name: "IX_SystemParameters_CountryId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropIndex(
                name: "IX_SystemParameters_GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropColumn(
                name: "SystemParametersId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropColumn(
                name: "GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemParameters",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_DefaultCountryId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "DefaultCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_DefaultGovernorateId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "DefaultGovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemParameters_Countries_DefaultCountryId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "DefaultCountryId",
                principalSchema: "HomeVisits",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemParameters_Governats_DefaultGovernorateId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "DefaultGovernorateId",
                principalSchema: "HomeVisits",
                principalTable: "Governats",
                principalColumn: "GovernateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemParameters_Countries_DefaultCountryId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemParameters_Governats_DefaultGovernorateId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SystemParameters",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropIndex(
                name: "IX_SystemParameters_DefaultCountryId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.DropIndex(
                name: "IX_SystemParameters_DefaultGovernorateId",
                schema: "HomeVisits",
                table: "SystemParameters");

            migrationBuilder.AddColumn<Guid>(
                name: "SystemParametersId",
                schema: "HomeVisits",
                table: "SystemParameters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "HomeVisits",
                table: "SystemParameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SystemParameters",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "SystemParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_ClientId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_CountryId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemParameters_GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "GovernateId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemParameters_Countries_CountryId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "CountryId",
                principalSchema: "HomeVisits",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemParameters_Governats_GovernateId",
                schema: "HomeVisits",
                table: "SystemParameters",
                column: "GovernateId",
                principalSchema: "HomeVisits",
                principalTable: "Governats",
                principalColumn: "GovernateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
