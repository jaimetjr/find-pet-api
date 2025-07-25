using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeChatEntity4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenAtByUserA",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SeenByUserA",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "SeenByUserB",
                table: "ChatMessages",
                newName: "WasSeen");

            migrationBuilder.RenameColumn(
                name: "SeenAtByUserB",
                table: "ChatMessages",
                newName: "WasSeenAt");

            migrationBuilder.AddColumn<string>(
                name: "SeenByClerkId",
                table: "ChatMessages",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenByClerkId",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "WasSeenAt",
                table: "ChatMessages",
                newName: "SeenAtByUserB");

            migrationBuilder.RenameColumn(
                name: "WasSeen",
                table: "ChatMessages",
                newName: "SeenByUserB");

            migrationBuilder.AddColumn<DateTime>(
                name: "SeenAtByUserA",
                table: "ChatMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SeenByUserA",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
