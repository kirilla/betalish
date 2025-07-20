using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartNumber = table.Column<int>(type: "int", nullable: false),
                    EndNumber = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceRanges_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRanges_ClientId",
                table: "InvoiceRanges",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceRanges");
        }
    }
}
