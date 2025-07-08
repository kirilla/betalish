using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClientEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientEventKind = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientEvents_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientEvents_ClientId",
                table: "ClientEvents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEvents_UserId",
                table: "ClientEvents",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientEvents");
        }
    }
}
