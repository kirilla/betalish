using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DebitInvoiceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceID",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "DebitInvoiceID",
                table: "BalanceRows",
                newName: "DebitInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceRows_DebitInvoiceID",
                table: "BalanceRows",
                newName: "IX_BalanceRows_DebitInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceId",
                table: "BalanceRows",
                column: "DebitInvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceId",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "DebitInvoiceId",
                table: "BalanceRows",
                newName: "DebitInvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceRows_DebitInvoiceId",
                table: "BalanceRows",
                newName: "IX_BalanceRows_DebitInvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceID",
                table: "BalanceRows",
                column: "DebitInvoiceID",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
