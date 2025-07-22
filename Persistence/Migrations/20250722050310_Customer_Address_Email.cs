using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Customer_Address_Email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Customer_Address1",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Address2",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_City",
                table: "Invoices",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Country",
                table: "Invoices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Email",
                table: "Invoices",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_ZipCode",
                table: "Invoices",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Address1",
                table: "InvoiceDrafts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Address2",
                table: "InvoiceDrafts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_City",
                table: "InvoiceDrafts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Country",
                table: "InvoiceDrafts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Email",
                table: "InvoiceDrafts",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_ZipCode",
                table: "InvoiceDrafts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Customers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_Address1",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Address2",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_City",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Country",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Email",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_ZipCode",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Customer_Address1",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_Address2",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_City",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_Country",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_Email",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Customer_ZipCode",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customers");
        }
    }
}
