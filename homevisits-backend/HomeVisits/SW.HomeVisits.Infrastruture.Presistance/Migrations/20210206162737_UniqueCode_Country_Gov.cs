using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class UniqueCode_Country_Gov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateIndex(
            //    name: "IX_Governats_Code",
            //    schema: "HomeVisits",
            //    table: "Governats",
            //    column: "Code",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Countries_Code",
            //    schema: "HomeVisits",
            //    table: "Countries",
            //    column: "Code",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Governats_Code",
            //    schema: "HomeVisits",
            //    table: "Governats");

            //migrationBuilder.DropIndex(
            //    name: "IX_Countries_Code",
            //    schema: "HomeVisits",
            //    table: "Countries");
        }
    }
}
