﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpiritualityAndFate.Models;

namespace SpiritualityAndFate.Migrations
{
    [DbContext(typeof(SAFContext))]
    partial class SAFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SpiritualityAndFate.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ItemId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SpiritualityAndFate.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AgeRange")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Species")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SpiritualityAndFate.Models.Item", b =>
                {
                    b.HasOne("SpiritualityAndFate.Models.Player", "Owner")
                        .WithMany("Inventory")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
