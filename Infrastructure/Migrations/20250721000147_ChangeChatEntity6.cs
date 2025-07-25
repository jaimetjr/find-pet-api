using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeChatEntity6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SeenByClerkId",
                table: "ChatMessages",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "RecipientId",
                table: "ChatMessages",
                type: "character varying(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "WasDelivered",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasDeliveredAt",
                table: "ChatMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RecipientId",
                table: "ChatMessages",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_RecipientId",
                table: "ChatMessages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_RecipientId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_RecipientId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "WasDelivered",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "WasDeliveredAt",
                table: "ChatMessages");

            migrationBuilder.AlterColumn<string>(
                name: "SeenByClerkId",
                table: "ChatMessages",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
