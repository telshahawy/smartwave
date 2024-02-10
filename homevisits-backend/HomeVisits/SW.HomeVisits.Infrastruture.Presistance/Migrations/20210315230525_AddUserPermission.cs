using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class AddUserPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "HomeVisits",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                schema: "HomeVisits",
                table: "Users",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStaticRole",
                schema: "HomeVisits",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                schema: "HomeVisits",
                table: "Permissions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                schema: "HomeVisits",
                table: "Permissions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAdditionalPermissions",
                schema: "HomeVisits",
                columns: table => new
                {
                    UserAdditionalPermissionId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdditionalPermissions", x => x.UserAdditionalPermissionId);
                    table.ForeignKey(
                        name: "FK_UserAdditionalPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "HomeVisits",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAdditionalPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAdditionalPermissions_Users_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserExcludedRolePermissions",
                schema: "HomeVisits",
                columns: table => new
                {
                    UserExcludedRolePermissionId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExcludedRolePermissions", x => x.UserExcludedRolePermissionId);
                    table.ForeignKey(
                        name: "FK_UserExcludedRolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "HomeVisits",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExcludedRolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "HomeVisits",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExcludedRolePermissions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserExcludedRolePermissions_Users_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "HomeVisits",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalPermissions_PermissionId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalPermissions_UserId",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalPermissions_UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_PermissionId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_RoleId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_UserId",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "HomeVisits",
                table: "Users",
                column: "RoleId",
                principalSchema: "HomeVisits",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "HomeVisits",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserAdditionalPermissions",
                schema: "HomeVisits");

            migrationBuilder.DropTable(
                name: "UserExcludedRolePermissions",
                schema: "HomeVisits");

            migrationBuilder.DropColumn(
                name: "IsStaticRole",
                schema: "HomeVisits",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ActionName",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                schema: "HomeVisits",
                table: "Permissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                schema: "HomeVisits",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "HomeVisits",
                table: "Users",
                column: "RoleId",
                principalSchema: "HomeVisits",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
