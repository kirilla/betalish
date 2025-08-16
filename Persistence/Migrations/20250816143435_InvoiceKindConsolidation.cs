using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceKindConsolidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "InvoiceDrafts");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceKind",
                table: "InvoiceDrafts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceKind",
                table: "InvoiceDrafts");

            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                table: "InvoiceDrafts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
