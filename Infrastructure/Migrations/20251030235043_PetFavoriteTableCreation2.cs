using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetFavoriteTableCreation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetFavorites_Users_UserId",
                table: "PetFavorites");

            migrationBuilder.DropIndex(
                name: "IX_PetFavorites_UserId",
                table: "PetFavorites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PetFavorites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PetFavorites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PetFavorites_UserId",
                table: "PetFavorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetFavorites_Users_UserId",
                table: "PetFavorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
