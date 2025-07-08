using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NetworkRule_Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prefix2",
                table: "NetworkRules",
                newName: "PrefixLength");

            migrationBuilder.RenameColumn(
                name: "BaseAddress2",
                table: "NetworkRules",
                newName: "BaseAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrefixLength",
                table: "NetworkRules",
                newName: "Prefix2");

            migrationBuilder.RenameColumn(
                name: "BaseAddress",
                table: "NetworkRules",
                newName: "BaseAddress2");
        }
    }
}
