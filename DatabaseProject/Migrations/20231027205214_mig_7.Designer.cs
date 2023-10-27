﻿// <auto-generated />
using System;
using DatabaseProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231027205214_mig_7")]
    partial class mig_7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatabaseProject.Entities.Adrress", b =>
                {
                    b.Property<int>("AdrressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdrressId"));

                    b.Property<string>("AdrressDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdrressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AdrressId");

                    b.HasIndex("UserId");

                    b.ToTable("Adrresses");
                });

            modelBuilder.Entity("DatabaseProject.Entities.Bank", b =>
                {
                    b.Property<int>("BankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BankId"));

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("DatabaseProject.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adrress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BornDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DatabaseProject.Entities.Adrress", b =>
                {
                    b.HasOne("DatabaseProject.Entities.User", "User")
                        .WithMany("Adrresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatabaseProject.Entities.Bank", b =>
                {
                    b.HasOne("DatabaseProject.Entities.User", "User")
                        .WithMany("Banks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatabaseProject.Entities.User", b =>
                {
                    b.Navigation("Adrresses");

                    b.Navigation("Banks");
                });
#pragma warning restore 612, 618
        }
    }
}
