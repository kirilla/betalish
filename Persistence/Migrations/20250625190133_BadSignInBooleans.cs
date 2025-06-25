using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BadSignInBooleans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exception",
                table: "BadSignIns");

            migrationBuilder.DropColumn(
                name: "InnerException",
                table: "BadSignIns");

            migrationBuilder.AddColumn<bool>(
                name: "BadPassword",
                table: "BadSignIns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BadUsername",
                table: "BadSignIns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OtherException",
                table: "BadSignIns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BadPassword",
                table: "BadSignIns");

            migrationBuilder.DropColumn(
                name: "BadUsername",
                table: "BadSignIns");

            migrationBuilder.DropColumn(
                name: "OtherException",
                table: "BadSignIns");

            migrationBuilder.AddColumn<string>(
                name: "Exception",
                table: "BadSignIns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InnerException",
                table: "BadSignIns",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
