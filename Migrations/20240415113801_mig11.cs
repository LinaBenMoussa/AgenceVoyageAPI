using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenceVoyage.Migrations
{
    /// <inheritdoc />
    public partial class mig11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Comptes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "Comptes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Comptes");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Comptes");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
