using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SplitBalanceRowInvoiceNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "BalanceRows",
                newName: "DebitInvoiceNumber");

            migrationBuilder.AddColumn<int>(
                name: "CreditInvoiceNumber",
                table: "BalanceRows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditInvoiceNumber",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "DebitInvoiceNumber",
                table: "BalanceRows",
                newName: "InvoiceNumber");
        }
    }
}
