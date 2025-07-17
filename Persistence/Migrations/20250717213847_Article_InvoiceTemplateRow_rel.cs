using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Article_InvoiceTemplateRow_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "InvoiceTemplateRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTemplateRows_ArticleId",
                table: "InvoiceTemplateRows",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTemplateRows_Articles_ArticleId",
                table: "InvoiceTemplateRows",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTemplateRows_Articles_ArticleId",
                table: "InvoiceTemplateRows");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTemplateRows_ArticleId",
                table: "InvoiceTemplateRows");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "InvoiceTemplateRows");
        }
    }
}
