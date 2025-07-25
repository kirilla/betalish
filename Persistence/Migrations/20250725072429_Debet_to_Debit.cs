using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Debet_to_Debit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_DebetInvoiceID",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "DebetInvoiceID",
                table: "BalanceRows",
                newName: "DebitInvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceRows_DebetInvoiceID",
                table: "BalanceRows",
                newName: "IX_BalanceRows_DebitInvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceID",
                table: "BalanceRows",
                column: "DebitInvoiceID",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceID",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "DebitInvoiceID",
                table: "BalanceRows",
                newName: "DebetInvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceRows_DebitInvoiceID",
                table: "BalanceRows",
                newName: "IX_BalanceRows_DebetInvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_DebetInvoiceID",
                table: "BalanceRows",
                column: "DebetInvoiceID",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
