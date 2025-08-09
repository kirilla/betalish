using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Ren_ClientEmailAccount_EmailAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientEmailAccounts_Clients_ClientId",
                table: "ClientEmailAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientEmailAccounts",
                table: "ClientEmailAccounts");

            migrationBuilder.RenameTable(
                name: "ClientEmailAccounts",
                newName: "EmailAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_ClientEmailAccounts_ClientId",
                table: "EmailAccounts",
                newName: "IX_EmailAccounts_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailAccounts",
                table: "EmailAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAccounts_Clients_ClientId",
                table: "EmailAccounts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAccounts_Clients_ClientId",
                table: "EmailAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailAccounts",
                table: "EmailAccounts");

            migrationBuilder.RenameTable(
                name: "EmailAccounts",
                newName: "ClientEmailAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAccounts_ClientId",
                table: "ClientEmailAccounts",
                newName: "IX_ClientEmailAccounts_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientEmailAccounts",
                table: "ClientEmailAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEmailAccounts_Clients_ClientId",
                table: "ClientEmailAccounts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
