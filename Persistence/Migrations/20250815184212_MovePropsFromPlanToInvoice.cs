using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MovePropsFromPlanToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collect",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "Demand",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "PaymentTermDays",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "InvoicePlans");

            migrationBuilder.AddColumn<bool>(
                name: "Collect",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Demand",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Distribution",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reminder",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collect",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Demand",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Distribution",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "Invoices");

            migrationBuilder.AddColumn<bool>(
                name: "Collect",
                table: "InvoicePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Demand",
                table: "InvoicePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTermDays",
                table: "InvoicePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Reminder",
                table: "InvoicePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
