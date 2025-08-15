using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveAllInvoicePlanPropsToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemandFee",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "MinToConsiderPaid",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "ReminderFee",
                table: "InvoicePlans");

            migrationBuilder.AddColumn<decimal>(
                name: "DemandFee",
                table: "Invoices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Interest",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MinToConsiderPaid",
                table: "Invoices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReminderFee",
                table: "Invoices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemandFee",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "MinToConsiderPaid",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReminderFee",
                table: "Invoices");

            migrationBuilder.AddColumn<decimal>(
                name: "DemandFee",
                table: "InvoicePlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Interest",
                table: "InvoicePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MinToConsiderPaid",
                table: "InvoicePlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReminderFee",
                table: "InvoicePlans",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }
    }
}
