using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betalish.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GutAndRepurpose_CustomerMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAddress",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "FromName",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "HtmlBody",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "ReplyToAddress",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "ReplyToName",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "Sent",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "TextBody",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "ToAddress",
                table: "CustomerMessages");

            migrationBuilder.DropColumn(
                name: "ToName",
                table: "CustomerMessages");

            migrationBuilder.RenameColumn(
                name: "EmailStatus",
                table: "CustomerMessages",
                newName: "CustomerMessageKind");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerMessageKind",
                table: "CustomerMessages",
                newName: "EmailStatus");

            migrationBuilder.AddColumn<string>(
                name: "FromAddress",
                table: "CustomerMessages",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromName",
                table: "CustomerMessages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HtmlBody",
                table: "CustomerMessages",
                type: "nvarchar(max)",
                maxLength: 16000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReplyToAddress",
                table: "CustomerMessages",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplyToName",
                table: "CustomerMessages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sent",
                table: "CustomerMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "CustomerMessages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextBody",
                table: "CustomerMessages",
                type: "nvarchar(max)",
                maxLength: 16000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToAddress",
                table: "CustomerMessages",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToName",
                table: "CustomerMessages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
