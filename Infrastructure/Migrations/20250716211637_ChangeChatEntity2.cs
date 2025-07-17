using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeChatEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_SenderId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_UserAId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_UserBId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_UserAId_UserBId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_UserBId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "UserAId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "UserBId",
                table: "ChatRooms");

            migrationBuilder.AddColumn<string>(
                name: "UserAClerkId",
                table: "ChatRooms",
                type: "character varying(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserBClerkId",
                table: "ChatRooms",
                type: "character varying(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "ChatMessages",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_UserAClerkId_UserBClerkId",
                table: "ChatRooms",
                columns: new[] { "UserAClerkId", "UserBClerkId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_UserBClerkId",
                table: "ChatRooms",
                column: "UserBClerkId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_SenderId",
                table: "ChatMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_UserAClerkId",
                table: "ChatRooms",
                column: "UserAClerkId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_UserBClerkId",
                table: "ChatRooms",
                column: "UserBClerkId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_SenderId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_UserAClerkId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_UserBClerkId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_UserAClerkId_UserBClerkId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_UserBClerkId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "UserAClerkId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "UserBClerkId",
                table: "ChatRooms");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAId",
                table: "ChatRooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserBId",
                table: "ChatRooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "ChatMessages",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_UserAId_UserBId",
                table: "ChatRooms",
                columns: new[] { "UserAId", "UserBId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_UserBId",
                table: "ChatRooms",
                column: "UserBId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_SenderId",
                table: "ChatMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_UserAId",
                table: "ChatRooms",
                column: "UserAId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_UserBId",
                table: "ChatRooms",
                column: "UserBId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
