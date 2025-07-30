using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BalanceRow_Split_Remake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_CreditInvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropIndex(
                name: "IX_BalanceRows_CreditInvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropIndex(
                name: "IX_BalanceRows_DebitInvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropColumn(
                name: "CreditInvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropColumn(
                name: "CreditInvoiceNumber",
                table: "BalanceRows");

            migrationBuilder.DropColumn(
                name: "DebitInvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropColumn(
                name: "PaymentsCreated",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "DebitInvoiceNumber",
                table: "BalanceRows",
                newName: "InvoiceId");

            migrationBuilder.AddColumn<Guid>(
                name: "RefGuid",
                table: "BalanceRows",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RefInvoiceNumber",
                table: "BalanceRows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceRows_InvoiceId",
                table: "BalanceRows",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_InvoiceId",
                table: "BalanceRows",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BalanceRows_Invoices_InvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropIndex(
                name: "IX_BalanceRows_InvoiceId",
                table: "BalanceRows");

            migrationBuilder.DropColumn(
                name: "RefGuid",
                table: "BalanceRows");

            migrationBuilder.DropColumn(
                name: "RefInvoiceNumber",
                table: "BalanceRows");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "BalanceRows",
                newName: "DebitInvoiceNumber");

            migrationBuilder.AddColumn<int>(
                name: "CreditInvoiceId",
                table: "BalanceRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreditInvoiceNumber",
                table: "BalanceRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DebitInvoiceId",
                table: "BalanceRows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentsCreated",
                table: "BalanceRows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceRows_CreditInvoiceId",
                table: "BalanceRows",
                column: "CreditInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceRows_DebitInvoiceId",
                table: "BalanceRows",
                column: "DebitInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_CreditInvoiceId",
                table: "BalanceRows",
                column: "CreditInvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BalanceRows_Invoices_DebitInvoiceId",
                table: "BalanceRows",
                column: "DebitInvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
