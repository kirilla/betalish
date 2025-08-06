using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoicePlanDueDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DemandDueDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReminderDueDate",
                table: "InvoicePlans",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemandDueDate",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "ReminderDueDate",
                table: "InvoicePlans");
        }
    }
}
