﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TasinmazProjesiAPI.DataAccess;

namespace TasinmazProjesiAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250118202944_UpdateUserTable")]
    partial class UpdateUserTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Il", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IlAdi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("iller");
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Ilce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IlId")
                        .HasColumnType("integer");

                    b.Property<string>("IlceAdi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IlId");

                    b.ToTable("ilceler");
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Aciklama")
                        .HasColumnType("text");

                    b.Property<string>("Durum")
                        .HasColumnType("text");

                    b.Property<string>("IslemTip")
                        .HasColumnType("text");

                    b.Property<DateTime>("TarihSaat")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserMail")
                        .HasColumnType("text");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Mahalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IlceId")
                        .HasColumnType("integer");

                    b.Property<string>("MahalleAdi")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IlceId");

                    b.ToTable("mahalleler");
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Tasinmaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ada")
                        .HasColumnType("text");

                    b.Property<string>("KoordinatBilgisi")
                        .HasColumnType("text");

                    b.Property<int>("MahalleId")
                        .HasColumnType("integer");

                    b.Property<string>("TasinmazAdres")
                        .HasColumnType("text");

                    b.Property<string>("TasinmazIsim")
                        .HasColumnType("text");

                    b.Property<string>("TasinmazNitelik")
                        .HasColumnType("text");

                    b.Property<int>("TasinmazParsel")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MahalleId");

                    b.HasIndex("UserId");

                    b.ToTable("tasinmazlar");
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("UserRole")
                        .HasColumnType("text");

                    b.Property<string>("userEmail")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Ilce", b =>
                {
                    b.HasOne("TasinmazProjesiAPI.Entitites.Concrete.Il", "Il")
                        .WithMany()
                        .HasForeignKey("IlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Mahalle", b =>
                {
                    b.HasOne("TasinmazProjesiAPI.Entitites.Concrete.Ilce", "Ilce")
                        .WithMany()
                        .HasForeignKey("IlceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TasinmazProjesiAPI.Entitites.Concrete.Tasinmaz", b =>
                {
                    b.HasOne("TasinmazProjesiAPI.Entitites.Concrete.Mahalle", "Mahalle")
                        .WithMany()
                        .HasForeignKey("MahalleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TasinmazProjesiAPI.Entitites.Concrete.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
