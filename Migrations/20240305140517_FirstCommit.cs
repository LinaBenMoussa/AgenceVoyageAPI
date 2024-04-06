using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenceVoyage.Migrations
{
    /// <inheritdoc />
    public partial class FirstCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    Id_compte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.Id_compte);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id_hotel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categorie = table.Column<int>(type: "int", nullable: false),
                    Nbre_chambres = table.Column<int>(type: "int", nullable: false),
                    Localisation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id_hotel);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id_client = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_compte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id_client);
                    table.ForeignKey(
                        name: "FK_Clients_Comptes_Id_compte",
                        column: x => x.Id_compte,
                        principalTable: "Comptes",
                        principalColumn: "Id_compte",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chambres",
                columns: table => new
                {
                    Id_chambre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nbre_personnes = table.Column<int>(type: "int", nullable: false),
                    Id_hotel = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chambres", x => x.Id_chambre);
                    table.ForeignKey(
                        name: "FK_Chambres_Hotels_Id_hotel",
                        column: x => x.Id_hotel,
                        principalTable: "Hotels",
                        principalColumn: "Id_hotel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id_photo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId_hotel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id_photo);
                    table.ForeignKey(
                        name: "FK_Photos_Hotels_Id_hotel",
                        column: x => x.HotelId_hotel,
                        principalTable: "Hotels",
                        principalColumn: "Id_hotel");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id_reservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_client = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_chambre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id_reservation);
                    table.ForeignKey(
                        name: "FK_Reservations_Chambres_Id_chambre",
                        column: x => x.Id_chambre,
                        principalTable: "Chambres",
                        principalColumn: "Id_chambre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_Id_client",
                        column: x => x.Id_client,
                        principalTable: "Clients",
                        principalColumn: "Id_client",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_Id_hotel",
                table: "Chambres",
                column: "Id_hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Id_compte",
                table: "Clients",
                column: "Id_compte");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Id_hotel",
                table: "Photos",
                column: "HotelId_hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Id_chambre",
                table: "Reservations",
                column: "Id_chambre");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Id_client",
                table: "Reservations",
                column: "Id_client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Chambres");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Comptes");
        }
    }
}
