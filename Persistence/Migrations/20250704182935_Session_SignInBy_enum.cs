using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Session_SignInBy_enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignInBy",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignInBy",
                table: "SessionRecords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SignInBy",
                table: "BadSignIns",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignInBy",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SignInBy",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "SignInBy",
                table: "BadSignIns");
        }
    }
}
