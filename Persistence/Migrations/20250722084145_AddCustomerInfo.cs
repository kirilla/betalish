using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerKind",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Orgnum",
                table: "Invoices",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Ssn10",
                table: "Invoices",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerKind",
                table: "InvoiceDrafts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "InvoiceDrafts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Orgnum",
                table: "InvoiceDrafts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Ssn10",
                table: "InvoiceDrafts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerKind",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Orgnum",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Ssn10",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CustomerKind",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_Orgnum",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_Ssn10",
                table: "InvoiceDrafts");
        }
    }
}
