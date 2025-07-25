using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeChatEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SeenAtByUserA",
                table: "ChatMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SeenAtByUserB",
                table: "ChatMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SeenByUserA",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SeenByUserB",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenAtByUserA",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SeenAtByUserB",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SeenByUserA",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SeenByUserB",
                table: "ChatMessages");
        }
    }
}
