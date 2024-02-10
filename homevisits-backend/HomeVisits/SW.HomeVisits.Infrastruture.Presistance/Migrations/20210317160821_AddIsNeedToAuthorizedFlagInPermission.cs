using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddIsNeedToAuthorizedFlagInPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModuleName",
                schema: "HomeVisits",
                table: "SystemPages",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                schema: "HomeVisits",
                table: "Permissions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "IsForDisplay",
                schema: "HomeVisits",
                table: "Permissions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedToAuthorized",
                schema: "HomeVisits",
                table: "Permissions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleName",
                schema: "HomeVisits",
                table: "SystemPages");

            migrationBuilder.DropColumn(
                name: "IsForDisplay",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsNeedToAuthorized",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "HomeVisits",
                table: "Permissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
