using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreUserInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Providers");

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Users",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Providers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Providers",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
