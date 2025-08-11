using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rel_Invoice_CustomerMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "CustomerMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerMessages_InvoiceId",
                table: "CustomerMessages",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerMessages_Invoices_InvoiceId",
                table: "CustomerMessages",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerMessages_Invoices_InvoiceId",
                table: "CustomerMessages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerMessages_InvoiceId",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "CustomerMessages");
        }
    }
}
