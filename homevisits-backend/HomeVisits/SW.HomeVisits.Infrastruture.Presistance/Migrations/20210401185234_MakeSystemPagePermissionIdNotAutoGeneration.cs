using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class MakeSystemPagePermissionIdNotAutoGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "HomeVisits",
                table: "SystemPagePermissions",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "HomeVisits",
                table: "SystemPagePermissions",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "SystemPagePermissionId",
            //    schema: "HomeVisits",
            //    table: "SystemPagePermissions",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "HomeVisits",
                table: "SystemPagePermissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "HomeVisits",
                table: "SystemPagePermissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            //migrationBuilder.AlterColumn<int>(
            //    name: "SystemPagePermissionId",
            //    schema: "HomeVisits",
            //    table: "SystemPagePermissions",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int))
            //    .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
