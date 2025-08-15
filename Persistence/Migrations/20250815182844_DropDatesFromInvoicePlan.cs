using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropDatesFromInvoicePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectDate",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DemandDate",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DemandDueDate",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "DistributionDate",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "ReminderDueDate",
                table: "InvoicePlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CollectDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DemandDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DemandDueDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DistributionDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReminderDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReminderDueDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);
        }
    }
}
