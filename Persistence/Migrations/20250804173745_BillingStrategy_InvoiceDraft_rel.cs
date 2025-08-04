using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BillingStrategy_InvoiceDraft_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillingStrategyId",
                table: "InvoiceDrafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDrafts_BillingStrategyId",
                table: "InvoiceDrafts",
                column: "BillingStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_BillingStrategies_BillingStrategyId",
                table: "InvoiceDrafts",
                column: "BillingStrategyId",
                principalTable: "BillingStrategies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_BillingStrategies_BillingStrategyId",
                table: "InvoiceDrafts");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_BillingStrategyId",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "BillingStrategyId",
                table: "InvoiceDrafts");
        }
    }
}
