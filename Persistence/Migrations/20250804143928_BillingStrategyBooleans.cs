using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BillingStrategyBooleans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Collect",
                table: "BillingStrategies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Demand",
                table: "BillingStrategies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Interest",
                table: "BillingStrategies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reminder",
                table: "BillingStrategies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collect",
                table: "BillingStrategies");

            migrationBuilder.DropColumn(
                name: "Demand",
                table: "BillingStrategies");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "BillingStrategies");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "BillingStrategies");
        }
    }
}
