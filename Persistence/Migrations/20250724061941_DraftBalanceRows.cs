using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DraftBalanceRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DraftBalanceRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceDraftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftBalanceRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftBalanceRows_InvoiceDrafts_InvoiceDraftId",
                        column: x => x.InvoiceDraftId,
                        principalTable: "InvoiceDrafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DraftBalanceRows_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DraftBalanceRows_InvoiceDraftId",
                table: "DraftBalanceRows",
                column: "InvoiceDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftBalanceRows_InvoiceId",
                table: "DraftBalanceRows",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraftBalanceRows");
        }
    }
}
