﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SuperHero.Migrations
{
    [DbContext(typeof(SuperHeroDbContext))]
    [Migration("20241202170501_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SuperHero.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("HeroId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Equipment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A sharp blade",
                            HeroId = 1,
                            Name = "Steel Sword",
                            TypeId = 1,
                            Weight = 3.5f
                        },
                        new
                        {
                            Id = 2,
                            Description = "Channel your magic power",
                            HeroId = 2,
                            Name = "Wizard Staff",
                            TypeId = 1,
                            Weight = 2f
                        },
                        new
                        {
                            Id = 3,
                            Description = "Light but protective",
                            HeroId = 3,
                            Name = "Leather Armor",
                            TypeId = 2,
                            Weight = 5f
                        },
                        new
                        {
                            Id = 4,
                            Description = "Restores 50 HP",
                            Name = "Health Potion",
                            TypeId = 4,
                            Weight = 0.5f
                        });
                });

            modelBuilder.Entity("SuperHero.Models.EquipmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EquipmentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Weapon"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Armor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Accessory"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Consumable"
                        });
                });

            modelBuilder.Entity("SuperHero.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HeroClassId")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("QuestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Heroes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A brave warrior",
                            HeroClassId = 1,
                            Level = 10,
                            Name = "Aragorn",
                            QuestId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "A wise mage",
                            HeroClassId = 2,
                            Level = 15,
                            Name = "Gandalf",
                            QuestId = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "A skilled archer",
                            HeroClassId = 3,
                            Level = 12,
                            Name = "Legolas"
                        });
                });

            modelBuilder.Entity("SuperHero.Models.HeroClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HeroClasses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Warrior"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mage"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Archer"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Rogue"
                        });
                });

            modelBuilder.Entity("SuperHero.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Quests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Protect the castle from invaders",
                            IsCompleted = false,
                            Name = "Defend the Castle"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Find the lost artifact in the ruins",
                            IsCompleted = false,
                            Name = "Retrieve the Ancient Artifact"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
