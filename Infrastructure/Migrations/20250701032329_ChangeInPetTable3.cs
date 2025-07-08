using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInPetTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_PetBreed_BreedId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_PetType_TypeId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Users_UserId",
                table: "Pet");

            migrationBuilder.DropForeignKey(
                name: "FK_PetBreed_PetType_TypeId",
                table: "PetBreed");

            migrationBuilder.DropForeignKey(
                name: "FK_PetImages_Pet_PetId",
                table: "PetImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetType",
                table: "PetType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetBreed",
                table: "PetBreed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.RenameTable(
                name: "PetType",
                newName: "PetTypes");

            migrationBuilder.RenameTable(
                name: "PetBreed",
                newName: "PetBreeds");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "Pets");

            migrationBuilder.RenameIndex(
                name: "IX_PetBreed_TypeId",
                table: "PetBreeds",
                newName: "IX_PetBreeds_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_UserId",
                table: "Pets",
                newName: "IX_Pets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_TypeId",
                table: "Pets",
                newName: "IX_Pets_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_BreedId",
                table: "Pets",
                newName: "IX_Pets_BreedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetTypes",
                table: "PetTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetBreeds",
                table: "PetBreeds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pets",
                table: "Pets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PetBreeds_PetTypes_TypeId",
                table: "PetBreeds",
                column: "TypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetImages_Pets_PetId",
                table: "PetImages",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetBreeds_BreedId",
                table: "Pets",
                column: "BreedId",
                principalTable: "PetBreeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetTypes_TypeId",
                table: "Pets",
                column: "TypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetBreeds_PetTypes_TypeId",
                table: "PetBreeds");

            migrationBuilder.DropForeignKey(
                name: "FK_PetImages_Pets_PetId",
                table: "PetImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetBreeds_BreedId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetTypes_TypeId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetTypes",
                table: "PetTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pets",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetBreeds",
                table: "PetBreeds");

            migrationBuilder.RenameTable(
                name: "PetTypes",
                newName: "PetType");

            migrationBuilder.RenameTable(
                name: "Pets",
                newName: "Pet");

            migrationBuilder.RenameTable(
                name: "PetBreeds",
                newName: "PetBreed");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_UserId",
                table: "Pet",
                newName: "IX_Pet_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_TypeId",
                table: "Pet",
                newName: "IX_Pet_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_BreedId",
                table: "Pet",
                newName: "IX_Pet_BreedId");

            migrationBuilder.RenameIndex(
                name: "IX_PetBreeds_TypeId",
                table: "PetBreed",
                newName: "IX_PetBreed_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetType",
                table: "PetType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetBreed",
                table: "PetBreed",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_PetBreed_BreedId",
                table: "Pet",
                column: "BreedId",
                principalTable: "PetBreed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_PetType_TypeId",
                table: "Pet",
                column: "TypeId",
                principalTable: "PetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Users_UserId",
                table: "Pet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ClerkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetBreed_PetType_TypeId",
                table: "PetBreed",
                column: "TypeId",
                principalTable: "PetType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PetImages_Pet_PetId",
                table: "PetImages",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
