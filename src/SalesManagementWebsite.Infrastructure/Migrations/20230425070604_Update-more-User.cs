using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagementWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatemoreUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IdentityCard",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentityCard",
                table: "Users");
        }
    }
}
