using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoicePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Reminder = table.Column<bool>(type: "bit", nullable: false),
                    Demand = table.Column<bool>(type: "bit", nullable: false),
                    Collect = table.Column<bool>(type: "bit", nullable: false),
                    PaymentTermDays = table.Column<int>(type: "int", nullable: false),
                    MinToConsiderPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Interest = table.Column<bool>(type: "bit", nullable: false),
                    DistributionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ReminderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DemandDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CollectDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SendByEmail = table.Column<bool>(type: "bit", nullable: false),
                    SendPostal = table.Column<bool>(type: "bit", nullable: false),
                    DistributionEmailSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DistributionInvoicePrinted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReminderEmailSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReminderInvoicePrinted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DemandEmailSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DemandInvoicePrinted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectEmailSent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollectInvoicePrinted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePlans_Invoices_Id",
                        column: x => x.Id,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePlans");
        }
    }
}
