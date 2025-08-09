using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_4real_CustomerMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagesToCustomer_Clients_ClientId",
                table: "MessagesToCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessagesToCustomer",
                table: "MessagesToCustomer");

            migrationBuilder.RenameTable(
                name: "MessagesToCustomer",
                newName: "CustomerMessages");

            migrationBuilder.RenameIndex(
                name: "IX_MessagesToCustomer_ClientId",
                table: "CustomerMessages",
                newName: "IX_CustomerMessages_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerMessages",
                table: "CustomerMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMessages_Clients_ClientId",
                table: "CustomerMessages",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMessages_Clients_ClientId",
                table: "CustomerMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerMessages",
                table: "CustomerMessages");

            migrationBuilder.RenameTable(
                name: "CustomerMessages",
                newName: "MessagesToCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerMessages_ClientId",
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
    }
}
