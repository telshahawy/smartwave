using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class allownullsforcolsinvisitstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MobileBatteryPercentage",
                schema: "HomeVisits",
                table: "VisitStatus",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                schema: "HomeVisits",
                table: "VisitStatus",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                schema: "HomeVisits",
                table: "VisitStatus",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceSerialNumber",
                schema: "HomeVisits",
                table: "VisitStatus",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                schema: "HomeVisits",
                table: "VisitStatus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                schema: "HomeVisits",
                table: "VisitStatus");

            migrationBuilder.AlterColumn<int>(
                name: "MobileBatteryPercentage",
                schema: "HomeVisits",
                table: "VisitStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                schema: "HomeVisits",
                table: "VisitStatus",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                schema: "HomeVisits",
                table: "VisitStatus",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceSerialNumber",
                schema: "HomeVisits",
                table: "VisitStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
