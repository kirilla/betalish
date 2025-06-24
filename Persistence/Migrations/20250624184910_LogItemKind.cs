using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LogItemKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "LogItems");

            migrationBuilder.AddColumn<int>(
                name: "LogItemKind",
                table: "LogItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogItemKind",
                table: "LogItems");

            migrationBuilder.AddColumn<int>(
                name: "Source",
                table: "LogItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
