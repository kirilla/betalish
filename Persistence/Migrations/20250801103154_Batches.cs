using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Batches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "InvoiceDrafts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BatchId",
                table: "Invoices",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDrafts_BatchId",
                table: "InvoiceDrafts",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDrafts_Batches_BatchId",
                table: "InvoiceDrafts",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Batches_BatchId",
                table: "Invoices",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDrafts_Batches_BatchId",
                table: "InvoiceDrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Batches_BatchId",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BatchId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_BatchId",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "InvoiceDrafts");
        }
    }
}
