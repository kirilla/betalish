using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SessionRecord_SessionEnd_enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasForced",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "WasReaped",
                table: "SessionRecords");

            migrationBuilder.AddColumn<int>(
                name: "SessionEnd",
                table: "SessionRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionEnd",
                table: "SessionRecords");

            migrationBuilder.AddColumn<bool>(
                name: "WasForced",
                table: "SessionRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WasReaped",
                table: "SessionRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
