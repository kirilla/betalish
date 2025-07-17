using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Customer_InvoiceDraft_rel_misc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Clients_ClientId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_Clients_ClientId",
                table: "InvoiceDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_Clients_ClientId",
                table: "InvoiceTemplates");

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
                name: "FK_Customers_Clients_ClientId",
                table: "Customers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_Clients_ClientId",
                table: "InvoiceDrafts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_Customers_CustomerId",
                table: "InvoiceDrafts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplates_Clients_ClientId",
                table: "InvoiceTemplates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Clients_ClientId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_Clients_ClientId",
                table: "InvoiceDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_Customers_CustomerId",
                table: "InvoiceDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplates_Clients_ClientId",
                table: "InvoiceTemplates");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_CustomerId",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InvoiceDrafts");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Clients_ClientId",
                table: "Customers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_Clients_ClientId",
                table: "InvoiceDrafts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_ClientId",
                table: "Invoices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplates_Clients_ClientId",
                table: "InvoiceTemplates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
