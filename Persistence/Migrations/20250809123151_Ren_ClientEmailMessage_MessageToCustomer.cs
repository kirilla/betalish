using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Ren_ClientEmailMessage_MessageToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientEmailMessages_Clients_ClientId",
                table: "ClientEmailMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientEmailMessages",
                table: "ClientEmailMessages");

            migrationBuilder.RenameTable(
                name: "ClientEmailMessages",
                newName: "MessagesToCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_ClientEmailMessages_ClientId",
                table: "MessagesToCustomer",
                newName: "IX_MessagesToCustomer_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessagesToCustomer",
                table: "MessagesToCustomer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesToCustomer_Clients_ClientId",
                table: "MessagesToCustomer",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagesToCustomer_Clients_ClientId",
                table: "MessagesToCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessagesToCustomer",
                table: "MessagesToCustomer");

            migrationBuilder.RenameTable(
                name: "MessagesToCustomer",
                newName: "ClientEmailMessages");

            migrationBuilder.RenameIndex(
                name: "IX_MessagesToCustomer_ClientId",
                table: "ClientEmailMessages",
                newName: "IX_ClientEmailMessages_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientEmailMessages",
                table: "ClientEmailMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientEmailMessages_Clients_ClientId",
                table: "ClientEmailMessages",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
