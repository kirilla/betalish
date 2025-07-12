using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropUser_SsnProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Ssn10",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Ssn10",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Ssn12",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SsnDate",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ssn10",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ssn12",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "SsnDate",
                table: "Users",
                type: "date",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Ssn10",
                table: "Users",
                column: "Ssn10",
                unique: true);
        }
    }
}
