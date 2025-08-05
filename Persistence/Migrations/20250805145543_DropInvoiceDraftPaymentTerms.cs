using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropInvoiceDraftPaymentTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTermDays",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "InvoiceDrafts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentTermDays",
                table: "InvoiceDrafts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "InvoiceDrafts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }
    }
}
