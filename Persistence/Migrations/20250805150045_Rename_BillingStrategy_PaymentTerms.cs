using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_BillingStrategy_PaymentTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingStrategies_Clients_ClientId",
                table: "BillingStrategies");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_BillingStrategies_BillingStrategyId",
                table: "InvoiceDrafts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillingStrategies",
                table: "BillingStrategies");

            migrationBuilder.RenameTable(
                name: "BillingStrategies",
                newName: "PaymentTerms");

            migrationBuilder.RenameIndex(
                name: "IX_BillingStrategies_ClientId",
                table: "PaymentTerms",
                newName: "IX_PaymentTerms_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentTerms",
                table: "PaymentTerms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_PaymentTerms_BillingStrategyId",
                table: "InvoiceDrafts",
                column: "BillingStrategyId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTerms_Clients_ClientId",
                table: "PaymentTerms",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_PaymentTerms_BillingStrategyId",
                table: "InvoiceDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTerms_Clients_ClientId",
                table: "PaymentTerms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentTerms",
                table: "PaymentTerms");

            migrationBuilder.RenameTable(
                name: "PaymentTerms",
                newName: "BillingStrategies");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTerms_ClientId",
                table: "BillingStrategies",
                newName: "IX_BillingStrategies_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillingStrategies",
                table: "BillingStrategies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingStrategies_Clients_ClientId",
                table: "BillingStrategies",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_BillingStrategies_BillingStrategyId",
                table: "InvoiceDrafts",
                column: "BillingStrategyId",
                principalTable: "BillingStrategies",
                principalColumn: "Id");
        }
    }
}
