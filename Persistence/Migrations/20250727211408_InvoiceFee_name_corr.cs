using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceFee_name_corr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesFees_Invoices_InvoiceId",
                table: "InvoicesFees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicesFees",
                table: "InvoicesFees");

            migrationBuilder.RenameTable(
                name: "InvoicesFees",
                newName: "InvoiceFees");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicesFees_InvoiceId",
                table: "InvoiceFees",
                newName: "IX_InvoiceFees_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceFees",
                table: "InvoiceFees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFees_Invoices_InvoiceId",
                table: "InvoiceFees",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceFees_Invoices_InvoiceId",
                table: "InvoiceFees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceFees",
                table: "InvoiceFees");

            migrationBuilder.RenameTable(
                name: "InvoiceFees",
                newName: "InvoicesFees");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceFees_InvoiceId",
                table: "InvoicesFees",
                newName: "IX_InvoicesFees_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicesFees",
                table: "InvoicesFees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesFees_Invoices_InvoiceId",
                table: "InvoicesFees",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
