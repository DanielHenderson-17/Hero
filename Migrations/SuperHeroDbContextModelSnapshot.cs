﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SuperHero.Migrations
{
    [DbContext(typeof(SuperHeroDbContext))]
    partial class SuperHeroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("isAvailable")
                        .HasColumnType("boolean");

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
                            Weight = 3.5f,
                            isAvailable = true
                        },
                        new
                        {
                            Id = 2,
                            Description = "Channel your magic power",
                            HeroId = 2,
                            Name = "Wizard Staff",
                            TypeId = 1,
                            Weight = 2f,
                            isAvailable = true
                        },
                        new
                        {
                            Id = 3,
                            Description = "Light but protective",
                            Name = "Leather Armor",
                            TypeId = 2,
                            Weight = 5f,
                            isAvailable = false
                        },
                        new
                        {
                            Id = 4,
                            Description = "Restores 50 HP",
                            Name = "Health Potion",
                            TypeId = 4,
                            Weight = 0.5f,
                            isAvailable = false
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

                    b.HasKey("Id");

                    b.HasIndex("HeroClassId");

                    b.ToTable("Heroes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A brave warrior",
                            HeroClassId = 1,
                            Level = 10,
                            Name = "Aragorn"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A wise mage",
                            HeroClassId = 2,
                            Level = 15,
                            Name = "Gandalf"
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

            modelBuilder.Entity("SuperHero.Models.HeroQuest", b =>
                {
                    b.Property<int>("HeroId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestId")
                        .HasColumnType("integer");

                    b.HasKey("HeroId", "QuestId");

                    b.HasIndex("QuestId");

                    b.ToTable("HeroQuests");

                    b.HasData(
                        new
                        {
                            HeroId = 1,
                            QuestId = 1
                        },
                        new
                        {
                            HeroId = 2,
                            QuestId = 2
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

            modelBuilder.Entity("SuperHero.Models.QuestEquipment", b =>
                {
                    b.Property<int>("QuestId")
                        .HasColumnType("integer");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("integer");

                    b.HasKey("QuestId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("QuestEquipments");

                    b.HasData(
                        new
                        {
                            QuestId = 1,
                            EquipmentId = 3
                        },
                        new
                        {
                            QuestId = 2,
                            EquipmentId = 4
                        });
                });

            modelBuilder.Entity("SuperHero.Models.Hero", b =>
                {
                    b.HasOne("SuperHero.Models.HeroClass", "HeroClass")
                        .WithMany()
                        .HasForeignKey("HeroClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HeroClass");
                });

            modelBuilder.Entity("SuperHero.Models.HeroQuest", b =>
                {
                    b.HasOne("SuperHero.Models.Hero", "Hero")
                        .WithMany("HeroQuests")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperHero.Models.Quest", "Quest")
                        .WithMany("HeroQuests")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("Quest");
                });

            modelBuilder.Entity("SuperHero.Models.QuestEquipment", b =>
                {
                    b.HasOne("SuperHero.Models.Equipment", "Equipment")
                        .WithMany("QuestEquipments")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperHero.Models.Quest", "Quest")
                        .WithMany("QuestEquipments")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Quest");
                });

            modelBuilder.Entity("SuperHero.Models.Equipment", b =>
                {
                    b.Navigation("QuestEquipments");
                });

            modelBuilder.Entity("SuperHero.Models.Hero", b =>
                {
                    b.Navigation("HeroQuests");
                });

            modelBuilder.Entity("SuperHero.Models.Quest", b =>
                {
                    b.Navigation("HeroQuests");

                    b.Navigation("QuestEquipments");
                });
#pragma warning restore 612, 618
        }
    }
}
