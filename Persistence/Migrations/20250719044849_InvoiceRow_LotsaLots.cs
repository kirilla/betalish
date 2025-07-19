using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceRow_LotsaLots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleName",
                table: "InvoiceRows",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ArticleNumber",
                table: "InvoiceRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCredit",
                table: "InvoiceRows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "InvoiceRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "InvoiceRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "InvoiceRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InvoiceRows",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatAmount",
                table: "InvoiceRows",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatPercentage",
                table: "InvoiceRows",
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
                name: "ArticleName",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "ArticleNumber",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "IsCredit",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "InvoiceRows");

            migrationBuilder.DropColumn(
                name: "VatPercentage",
                table: "InvoiceRows");
        }
    }
}
