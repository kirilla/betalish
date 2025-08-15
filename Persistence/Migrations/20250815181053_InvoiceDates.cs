using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "CollectDate",
                table: "Invoices",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DemandDate",
                table: "Invoices",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DemandDueDate",
                table: "Invoices",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReminderDate",
                table: "Invoices",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReminderDueDate",
                table: "Invoices",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DemandDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DemandDueDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReminderDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReminderDueDate",
                table: "Invoices");
        }
    }
}
