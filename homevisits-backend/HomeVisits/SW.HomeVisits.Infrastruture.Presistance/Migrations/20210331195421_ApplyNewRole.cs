using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class ApplyNewRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_SystemPages_SystemPageId",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionUsages_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "PermissionUsages");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAdditionalPermissions_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExcludedRolePermissions_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserExcludedRolePermissions_PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserAdditionalPermissions_PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_PermissionUsages_PermissionId",
                schema: "HomeVisits",
                table: "PermissionUsages");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_SystemPageId",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropColumn(
                name: "ModuleName",
                schema: "HomeVisits",
                table: "SystemPages");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "ActionName",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsForDisplay",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsNeedToAuthorized",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SystemPageId",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasURL",
                schema: "HomeVisits",
                table: "SystemPages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                schema: "HomeVisits",
                table: "SystemPages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemPagePermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SystemPagePermissions",
                schema: "HomeVisits",
                columns: table => new
                {
                    SystemPagePermissionId = table.Column<int>(nullable: false),
                        //.Annotation("SqlServer:Identity", "1, 1"),
                    SystemPageId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPagePermissions", x => x.SystemPagePermissionId);
                    table.ForeignKey(
                        name: "FK_SystemPagePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "HomeVisits",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemPagePermissions_SystemPages_SystemPageId",
                        column: x => x.SystemPageId,
                        principalSchema: "HomeVisits",
                        principalTable: "SystemPages",
                        principalColumn: "SystemPageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "SystemPagePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalPermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "SystemPagePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                column: "SystemPagePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPagePermissions_PermissionId",
                schema: "HomeVisits",
                table: "SystemPagePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPagePermissions_SystemPageId",
                schema: "HomeVisits",
                table: "SystemPagePermissions",
                column: "SystemPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_SystemPagePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                column: "SystemPagePermissionId",
                principalSchema: "HomeVisits",
                principalTable: "SystemPagePermissions",
                principalColumn: "SystemPagePermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdditionalPermissions_SystemPagePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "SystemPagePermissionId",
                principalSchema: "HomeVisits",
                principalTable: "SystemPagePermissions",
                principalColumn: "SystemPagePermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExcludedRolePermissions_SystemPagePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "SystemPagePermissionId",
                principalSchema: "HomeVisits",
                principalTable: "SystemPagePermissions",
                principalColumn: "SystemPagePermissionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_SystemPagePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAdditionalPermissions_SystemPagePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExcludedRolePermissions_SystemPagePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropTable(
                name: "SystemPagePermissions",
                schema: "HomeVisits");

            migrationBuilder.DropIndex(
                name: "IX_UserExcludedRolePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserAdditionalPermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_SystemPagePermissionId",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropColumn(
                name: "SystemPagePermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropColumn(
                name: "HasURL",
                schema: "HomeVisits",
                table: "SystemPages");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "HomeVisits",
                table: "SystemPages");

            migrationBuilder.DropColumn(
                name: "SystemPagePermissionId",
                schema: "HomeVisits",
                table: "RolePermissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModuleName",
                schema: "HomeVisits",
                table: "SystemPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                schema: "HomeVisits",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                schema: "HomeVisits",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForDisplay",
                schema: "HomeVisits",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedToAuthorized",
                schema: "HomeVisits",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SystemPageId",
                schema: "HomeVisits",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalPermissions_PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionUsages_PermissionId",
                schema: "HomeVisits",
                table: "PermissionUsages",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SystemPageId",
                schema: "HomeVisits",
                table: "Permissions",
                column: "SystemPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_SystemPages_SystemPageId",
                schema: "HomeVisits",
                table: "Permissions",
                column: "SystemPageId",
                principalSchema: "HomeVisits",
                principalTable: "SystemPages",
                principalColumn: "SystemPageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionUsages_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "PermissionUsages",
                column: "PermissionId",
                principalSchema: "HomeVisits",
                principalTable: "Permissions",
                principalColumn: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "RolePermissions",
                column: "PermissionId",
                principalSchema: "HomeVisits",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdditionalPermissions_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "PermissionId",
                principalSchema: "HomeVisits",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExcludedRolePermissions_Permissions_PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "PermissionId",
                principalSchema: "HomeVisits",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
