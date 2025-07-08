using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInPetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Users_UserId",
                table: "Pet");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pet",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_ClerkId",
                table: "Users",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClerkId",
                table: "Users",
                column: "ClerkId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Users_UserId",
                table: "Pet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Users_UserId",
                table: "Pet");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_ClerkId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClerkId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Pet",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Users_UserId",
                table: "Pet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
