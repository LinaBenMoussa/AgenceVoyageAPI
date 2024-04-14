using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenceVoyage.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambres_Hotels_Id_hotel",
                table: "Chambres");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Comptes_Id_compte",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Id_compte",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Chambres_Id_hotel",
                table: "Chambres");

            migrationBuilder.AddColumn<int>(
                name: "HotelId_hotel",
                table: "Chambres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_HotelId_hotel",
                table: "Chambres",
                column: "HotelId_hotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambres_Hotels_HotelId_hotel",
                table: "Chambres",
                column: "HotelId_hotel",
                principalTable: "Hotels",
                principalColumn: "Id_hotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chambres_Hotels_HotelId_hotel",
                table: "Chambres");

            migrationBuilder.DropIndex(
                name: "IX_Chambres_HotelId_hotel",
                table: "Chambres");

            migrationBuilder.DropColumn(
                name: "HotelId_hotel",
                table: "Chambres");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Id_compte",
                table: "Clients",
                column: "Id_compte");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_Id_hotel",
                table: "Chambres",
                column: "Id_hotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Chambres_Hotels_Id_hotel",
                table: "Chambres",
                column: "Id_hotel",
                principalTable: "Hotels",
                principalColumn: "Id_hotel",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Comptes_Id_compte",
                table: "Clients",
                column: "Id_compte",
                principalTable: "Comptes",
                principalColumn: "Id_compte",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
