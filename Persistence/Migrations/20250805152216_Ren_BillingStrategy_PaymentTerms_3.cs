using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Ren_BillingStrategy_PaymentTerms_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_PaymentTerms_BillingStrategyId",
                table: "InvoiceDrafts");

            migrationBuilder.RenameColumn(
                name: "BillingStrategyId",
                table: "InvoiceDrafts",
                newName: "PaymentTermsId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDrafts_BillingStrategyId",
                table: "InvoiceDrafts",
                newName: "IX_InvoiceDrafts_PaymentTermsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_PaymentTerms_PaymentTermsId",
                table: "InvoiceDrafts",
                column: "PaymentTermsId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_PaymentTerms_PaymentTermsId",
                table: "InvoiceDrafts");

            migrationBuilder.RenameColumn(
                name: "PaymentTermsId",
                table: "InvoiceDrafts",
                newName: "BillingStrategyId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDrafts_PaymentTermsId",
                table: "InvoiceDrafts",
                newName: "IX_InvoiceDrafts_BillingStrategyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_PaymentTerms_BillingStrategyId",
                table: "InvoiceDrafts",
                column: "BillingStrategyId",
                principalTable: "PaymentTerms",
                principalColumn: "Id");
        }
    }
}
