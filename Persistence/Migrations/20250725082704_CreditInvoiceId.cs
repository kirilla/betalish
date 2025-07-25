using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreditInvoiceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_CreditInvoiceID",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "CreditInvoiceID",
                table: "BalanceRows",
                newName: "CreditInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceRows_CreditInvoiceID",
                table: "BalanceRows",
                newName: "IX_BalanceRows_CreditInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_CreditInvoiceId",
                table: "BalanceRows",
                column: "CreditInvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_CreditInvoiceId",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "CreditInvoiceId",
                table: "BalanceRows",
                newName: "CreditInvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceRows_CreditInvoiceId",
                table: "BalanceRows",
                newName: "IX_BalanceRows_CreditInvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_CreditInvoiceID",
                table: "BalanceRows",
                column: "CreditInvoiceID",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
