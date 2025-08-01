using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Client_Batches_rel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Batches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ClientId",
                table: "Batches",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batches_Clients_ClientId",
                table: "Batches",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batches_Clients_ClientId",
                table: "Batches");

            migrationBuilder.DropIndex(
                name: "IX_Batches_ClientId",
                table: "Batches");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Batches");
        }
    }
}
