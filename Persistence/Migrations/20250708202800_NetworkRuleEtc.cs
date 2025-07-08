using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NetworkRuleEtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseAddress2",
                table: "NetworkRules",
                type: "nvarchar(43)",
                maxLength: 43,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prefix2",
                table: "NetworkRules",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseAddress2",
                table: "NetworkRules");

            migrationBuilder.DropColumn(
                name: "Prefix2",
                table: "NetworkRules");
        }
    }
}
