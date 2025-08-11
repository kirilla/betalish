using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CustomerMessageMeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerMessageKind",
                table: "CustomerMessages",
                newName: "MessageKind");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "CustomerMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistributionMethod",
                table: "CustomerMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "CustomerMessages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "DistributionMethod",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "CustomerMessages");

            migrationBuilder.RenameColumn(
                name: "MessageKind",
                table: "CustomerMessages",
                newName: "CustomerMessageKind");
        }
    }
}
