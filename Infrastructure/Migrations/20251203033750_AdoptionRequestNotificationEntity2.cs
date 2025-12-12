using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdoptionRequestNotificationEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Users_AdopterId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Users_OwnerId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_AdopterId",
                table: "AdoptionRequests");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_OwnerId",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AdopterId",
                table: "AdoptionRequests");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AdoptionRequests");

            migrationBuilder.AlterColumn<string>(
                name: "UserClerkId",
                table: "Notifications",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerClerkId",
                table: "AdoptionRequests",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AdopterClerkId",
                table: "AdoptionRequests",
                type: "character varying(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserClerkId",
                table: "Notifications",
                column: "UserClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_AdopterClerkId",
                table: "AdoptionRequests",
                column: "AdopterClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_OwnerClerkId",
                table: "AdoptionRequests",
                column: "OwnerClerkId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Users_AdopterClerkId",
                table: "AdoptionRequests",
                column: "AdopterClerkId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Users_OwnerClerkId",
                table: "AdoptionRequests",
                column: "OwnerClerkId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserClerkId",
                table: "Notifications",
                column: "UserClerkId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Users_AdopterClerkId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequests_Users_OwnerClerkId",
                table: "AdoptionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserClerkId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserClerkId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_AdopterClerkId",
                table: "AdoptionRequests");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequests_OwnerClerkId",
                table: "AdoptionRequests");

            migrationBuilder.AlterColumn<string>(
                name: "UserClerkId",
                table: "Notifications",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Notifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "OwnerClerkId",
                table: "AdoptionRequests",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AlterColumn<string>(
                name: "AdopterClerkId",
                table: "AdoptionRequests",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)");

            migrationBuilder.AddColumn<Guid>(
                name: "AdopterId",
                table: "AdoptionRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "AdoptionRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_AdopterId",
                table: "AdoptionRequests",
                column: "AdopterId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequests_OwnerId",
                table: "AdoptionRequests",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Users_AdopterId",
                table: "AdoptionRequests",
                column: "AdopterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequests_Users_OwnerId",
                table: "AdoptionRequests",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
