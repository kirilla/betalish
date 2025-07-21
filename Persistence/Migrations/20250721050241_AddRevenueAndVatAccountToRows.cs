using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRevenueAndVatAccountToRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Articles",
                newName: "RevenueAccount");

            migrationBuilder.AddColumn<string>(
                name: "RevenueAccount",
                table: "InvoiceRows",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VatAccount",
                table: "InvoiceRows",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RevenueAccount",
                table: "InvoiceDraftRows",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VatAccount",
                table: "InvoiceDraftRows",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevenueAccount",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "VatAccount",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "RevenueAccount",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "VatAccount",
                table: "InvoiceDraftRows");

            migrationBuilder.RenameColumn(
                name: "RevenueAccount",
                table: "Articles",
                newName: "Account");
        }
    }
}
