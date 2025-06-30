using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClientSessionRecordRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "SessionRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionRecords_ClientId",
                table: "SessionRecords",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionRecords_Clients_ClientId",
                table: "SessionRecords",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionRecords_Clients_ClientId",
                table: "SessionRecords");

            migrationBuilder.DropIndex(
                name: "IX_SessionRecords_ClientId",
                table: "SessionRecords");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "SessionRecords");
        }
    }
}
