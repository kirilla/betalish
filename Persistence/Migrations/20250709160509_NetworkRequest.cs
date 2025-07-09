using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NetworkRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedRequests",
                table: "BlockedRequests");

            migrationBuilder.RenameTable(
                name: "BlockedRequests",
                newName: "NetworkRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NetworkRequests",
                table: "NetworkRequests",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NetworkRequests",
                table: "NetworkRequests");

            migrationBuilder.RenameTable(
                name: "NetworkRequests",
                newName: "BlockedRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedRequests",
                table: "BlockedRequests",
                column: "Id");
        }
    }
}
