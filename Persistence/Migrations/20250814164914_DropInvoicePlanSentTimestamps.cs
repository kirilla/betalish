using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropInvoicePlanSentTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectEmailSent",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "CollectInvoicePrinted",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DemandEmailSent",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DemandInvoicePrinted",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DistributionEmailSent",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DistributionInvoicePrinted",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "ReminderEmailSent",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "ReminderInvoicePrinted",
                table: "InvoicePlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CollectEmailSent",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CollectInvoicePrinted",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DemandEmailSent",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DemandInvoicePrinted",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DistributionEmailSent",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DistributionInvoicePrinted",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderEmailSent",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderInvoicePrinted",
                table: "InvoicePlans",
                type: "datetime2",
                nullable: true);
        }
    }
}
