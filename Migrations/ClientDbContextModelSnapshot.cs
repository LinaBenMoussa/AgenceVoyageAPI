﻿// <auto-generated />
using System;
using AgenceVoyage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgenceVoyage.Migrations
{
    [DbContext(typeof(ClientDbContext))]
    partial class ClientDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AgenceVoyage.Models.Chambre", b =>
                {
                    b.Property<int>("Id_chambre")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_chambre"));

                    b.Property<int>("Id_hotel")
                        .HasColumnType("int");

                    b.Property<int>("Nbre_personnes")
                        .HasColumnType("int");

                    b.Property<float>("Prix")
                        .HasColumnType("real");

                    b.Property<int>("nom")
                        .HasColumnType("int");

                    b.HasKey("Id_chambre");

                    b.HasIndex("Id_hotel");

                    b.ToTable("Chambres");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Client", b =>
                {
                    b.Property<int>("Id_client")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_client"));

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_compte")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Telephone")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id_client");

                    b.HasIndex("Id_compte");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Compte", b =>
                {
                    b.Property<int>("Id_compte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_compte"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_compte");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Destination", b =>
                {
                    b.Property<int>("Id_destination")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_destination"));

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_destination");

                    b.ToTable("Destination");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Hotel", b =>
                {
                    b.Property<int>("Id_hotel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_hotel"));

                    b.Property<int>("Categorie")
                        .HasColumnType("int");

                    b.Property<int>("Id_destination")
                        .HasColumnType("int");

                    b.Property<string>("Localisation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nbre_chambres")
                        .HasColumnType("int");

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_hotel");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Photo", b =>
                {
                    b.Property<int>("Id_photo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_photo"));

                    b.Property<int?>("HotelId_hotel")
                        .HasColumnType("int");

                    b.Property<int>("Id_hotel")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_photo");

                    b.HasIndex("HotelId_hotel");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Reservation", b =>
                {
                    b.Property<int>("Id_reservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_reservation"));

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_chambre")
                        .HasColumnType("int");

                    b.Property<int>("Id_client")
                        .HasColumnType("int");

                    b.HasKey("Id_reservation");

                    b.HasIndex("Id_chambre");

                    b.HasIndex("Id_client");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Chambre", b =>
                {
                    b.HasOne("AgenceVoyage.Models.Hotel", "Hotel")
                        .WithMany("Chambres")
                        .HasForeignKey("Id_hotel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Client", b =>
                {
                    b.HasOne("AgenceVoyage.Models.Compte", "Compte")
                        .WithMany()
                        .HasForeignKey("Id_compte")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Photo", b =>
                {
                    b.HasOne("AgenceVoyage.Models.Hotel", null)
                        .WithMany("Photos")
                        .HasForeignKey("HotelId_hotel");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Reservation", b =>
                {
                    b.HasOne("AgenceVoyage.Models.Chambre", "Chambre")
                        .WithMany()
                        .HasForeignKey("Id_chambre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgenceVoyage.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("Id_client")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chambre");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("AgenceVoyage.Models.Hotel", b =>
                {
                    b.Navigation("Chambres");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
