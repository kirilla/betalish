using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rel_Client_EmailMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "EmailMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessages_ClientId",
                table: "EmailMessages",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailMessages_Clients_ClientId",
                table: "EmailMessages",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailMessages_Clients_ClientId",
                table: "EmailMessages");

            migrationBuilder.DropIndex(
                name: "IX_EmailMessages_ClientId",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "EmailMessages");
        }
    }
}
