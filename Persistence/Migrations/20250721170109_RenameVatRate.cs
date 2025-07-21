using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameVatRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VatPercentage",
                table: "InvoiceRows",
                newName: "VatRate");

            migrationBuilder.RenameColumn(
                name: "VatPercentage",
                table: "InvoiceDraftRows",
                newName: "VatRate");

            migrationBuilder.RenameColumn(
                name: "VatValue",
                table: "Articles",
                newName: "VatRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "InvoiceRows",
                newName: "VatPercentage");

            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "InvoiceDraftRows",
                newName: "VatPercentage");

            migrationBuilder.RenameColumn(
                name: "VatRate",
                table: "Articles",
                newName: "VatValue");
        }
    }
}
