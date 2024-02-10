using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddIamNotSureFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IamNotSure",
                schema: "HomeVisits",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RelativeDateOfBirth",
                schema: "HomeVisits",
                table: "Visits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IamNotSure",
                schema: "HomeVisits",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "RelativeDateOfBirth",
                schema: "HomeVisits",
                table: "Visits");
        }
    }
}
