using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceAccountingRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccountings_Invoices_InvoiceId",
                table: "InvoiceAccountings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceAccountings",
                table: "InvoiceAccountings");

            migrationBuilder.RenameTable(
                name: "InvoiceAccountings",
                newName: "InvoiceAccountingRows");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceAccountings_InvoiceId",
                table: "InvoiceAccountingRows",
                newName: "IX_InvoiceAccountingRows_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceAccountingRows",
                table: "InvoiceAccountingRows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccountingRows_Invoices_InvoiceId",
                table: "InvoiceAccountingRows",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceAccountingRows_Invoices_InvoiceId",
                table: "InvoiceAccountingRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceAccountingRows",
                table: "InvoiceAccountingRows");

            migrationBuilder.RenameTable(
                name: "InvoiceAccountingRows",
                newName: "InvoiceAccountings");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceAccountingRows_InvoiceId",
                table: "InvoiceAccountings",
                newName: "IX_InvoiceAccountings_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceAccountings",
                table: "InvoiceAccountings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceAccountings_Invoices_InvoiceId",
                table: "InvoiceAccountings",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
