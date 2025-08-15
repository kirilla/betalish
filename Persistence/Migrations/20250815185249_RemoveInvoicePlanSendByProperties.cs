using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInvoicePlanSendByProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendByEmail",
                table: "InvoicePlans");

            migrationBuilder.DropColumn(
                name: "SendPostal",
                table: "InvoicePlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendByEmail",
                table: "InvoicePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendPostal",
                table: "InvoicePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
