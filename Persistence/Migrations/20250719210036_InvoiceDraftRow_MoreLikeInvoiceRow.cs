using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceDraftRow_MoreLikeInvoiceRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "ArticleName",
                table: "InvoiceDraftRows",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ArticleNumber",
                table: "InvoiceDraftRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                table: "InvoiceDraftRows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "InvoiceDraftRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "InvoiceDraftRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InvoiceDraftRows",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceDraftRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatAmount",
                table: "InvoiceDraftRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatPercentage",
                table: "InvoiceDraftRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "ArticleName",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "ArticleNumber",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "VatPercentage",
                table: "InvoiceDraftRows");
        }
    }
}
