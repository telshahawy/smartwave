using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SW.HomeVisits.Infrastruture.Presistance.Migrations
{
    public partial class UpdateUserPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAdditionalPermissions_Users_UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExcludedRolePermissions_Users_UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserExcludedRolePermissions_UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_UserAdditionalPermissions_UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExcludedRolePermissions_UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalPermissions_UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdditionalPermissions_Users_UserId1",
                schema: "HomeVisits",
                table: "UserAdditionalPermissions",
                column: "UserId1",
                principalSchema: "HomeVisits",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExcludedRolePermissions_Users_UserId1",
                schema: "HomeVisits",
                table: "UserExcludedRolePermissions",
                column: "UserId1",
                principalSchema: "HomeVisits",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
