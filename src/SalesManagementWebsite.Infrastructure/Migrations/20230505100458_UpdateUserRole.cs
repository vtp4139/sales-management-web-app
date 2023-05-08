using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RolesId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UsersId",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserRoles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RolesId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRoles",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRoles",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                newName: "IX_UserRoles_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RolesId",
                table: "UserRoles",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UsersId",
                table: "UserRoles",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
