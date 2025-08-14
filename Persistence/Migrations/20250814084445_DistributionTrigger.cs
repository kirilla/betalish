using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DistributionTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionMessages_Invoices_InvoiceId",
                table: "DistributionMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DistributionMessages",
                table: "DistributionMessages");

            migrationBuilder.RenameTable(
                name: "DistributionMessages",
                newName: "DistributionTriggers");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "DistributionTriggers",
                newName: "Distributed");

            migrationBuilder.RenameIndex(
                name: "IX_DistributionMessages_InvoiceId",
                table: "DistributionTriggers",
                newName: "IX_DistributionTriggers_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DistributionTriggers",
                table: "DistributionTriggers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionTriggers_Invoices_InvoiceId",
                table: "DistributionTriggers",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionTriggers_Invoices_InvoiceId",
                table: "DistributionTriggers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DistributionTriggers",
                table: "DistributionTriggers");

            migrationBuilder.RenameTable(
                name: "DistributionTriggers",
                newName: "DistributionMessages");

            migrationBuilder.RenameColumn(
                name: "Distributed",
                table: "DistributionMessages",
                newName: "Updated");

            migrationBuilder.RenameIndex(
                name: "IX_DistributionTriggers_InvoiceId",
                table: "DistributionMessages",
                newName: "IX_DistributionMessages_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DistributionMessages",
                table: "DistributionMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionMessages_Invoices_InvoiceId",
                table: "DistributionMessages",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
