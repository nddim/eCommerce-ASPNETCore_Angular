﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebshopApi.Data;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231230193356_dodaniKupacAdminRacun")]
    partial class dodaniKupacAdminRacun
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPI.Data.Models.Artikl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artikl");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Brend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brend");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kategorije.GlavnaKategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GlavnaKategorija");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kategorije.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GlavnaKategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GlavnaKategorijaID");

                    b.ToTable("Kategorija");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kategorije.Potkategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaID");

                    b.ToTable("Potkategorija");
                });

            modelBuilder.Entity("WebAPI.Data.Models.KorisnickiRacun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatumKreiranja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatumModifikovanja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KorisnickiRacuni");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("WebAPI.Data.Models.Proizvod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrendID")
                        .HasColumnType("int");

                    b.Property<int>("BrojKlikova")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PocetnaCijena")
                        .HasColumnType("real");

                    b.Property<int>("PocetnaKolicina")
                        .HasColumnType("int");

                    b.Property<int>("Popust")
                        .HasColumnType("int");

                    b.Property<int>("PotkategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("SlikaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrendID");

                    b.HasIndex("PotkategorijaID");

                    b.ToTable("Proizvod");
                });

            modelBuilder.Entity("WebAPI.Data.Models.ProizvodSlika", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProizvodId")
                        .HasColumnType("int");

                    b.Property<string>("SlikaUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProizvodSlika");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Administrator", b =>
                {
                    b.HasBaseType("WebAPI.Data.Models.KorisnickiRacun");

                    b.Property<DateTime?>("ZadnjiLogin")
                        .HasColumnType("datetime2");

                    b.ToTable("Administratori");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kupac", b =>
                {
                    b.HasBaseType("WebAPI.Data.Models.KorisnickiRacun");

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Kupac");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kategorije.Kategorija", b =>
                {
                    b.HasOne("WebAPI.Data.Models.Kategorije.GlavnaKategorija", "GlavnaKategorija")
                        .WithMany()
                        .HasForeignKey("GlavnaKategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GlavnaKategorija");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kategorije.Potkategorija", b =>
                {
                    b.HasOne("WebAPI.Data.Models.Kategorije.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategorija");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Proizvod", b =>
                {
                    b.HasOne("WebAPI.Data.Models.Brend", "Brend")
                        .WithMany()
                        .HasForeignKey("BrendID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebAPI.Data.Models.Kategorije.Potkategorija", "Potkategorija")
                        .WithMany()
                        .HasForeignKey("PotkategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brend");

                    b.Navigation("Potkategorija");
                });

            modelBuilder.Entity("WebAPI.Data.Models.Administrator", b =>
                {
                    b.HasOne("WebAPI.Data.Models.KorisnickiRacun", null)
                        .WithOne()
                        .HasForeignKey("WebAPI.Data.Models.Administrator", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebAPI.Data.Models.Kupac", b =>
                {
                    b.HasOne("WebAPI.Data.Models.KorisnickiRacun", null)
                        .WithOne()
                        .HasForeignKey("WebAPI.Data.Models.Kupac", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
