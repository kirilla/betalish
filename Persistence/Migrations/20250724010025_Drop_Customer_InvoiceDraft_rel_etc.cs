using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Drop_Customer_InvoiceDraft_rel_etc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_Customers_CustomerId",
                table: "InvoiceDrafts");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_CustomerId",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InvoiceDrafts");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Invoices",
                newName: "CustomerId_Hint");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                newName: "IX_Invoices_CustomerId_Hint");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId_Hint",
                table: "InvoiceDrafts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDrafts_CustomerId_Hint",
                table: "InvoiceDrafts",
                column: "CustomerId_Hint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_CustomerId_Hint",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "CustomerId_Hint",
                table: "InvoiceDrafts");

            migrationBuilder.RenameColumn(
                name: "CustomerId_Hint",
                table: "Invoices",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CustomerId_Hint",
                table: "Invoices",
                newName: "IX_Invoices_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "InvoiceDrafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDrafts_CustomerId",
                table: "InvoiceDrafts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_Customers_CustomerId",
                table: "InvoiceDrafts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
