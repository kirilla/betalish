using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Drop_InvoiceDraftRow_Article_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDraftRows_Articles_ArticleId",
                table: "InvoiceDraftRows");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDraftRows_ArticleId",
                table: "InvoiceDraftRows");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "InvoiceDraftRows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "InvoiceDraftRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDraftRows_ArticleId",
                table: "InvoiceDraftRows",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDraftRows_Articles_ArticleId",
                table: "InvoiceDraftRows",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
