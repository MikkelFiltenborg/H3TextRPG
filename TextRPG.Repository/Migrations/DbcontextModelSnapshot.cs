﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TextRPG.Repository.Server;

#nullable disable

namespace TextRPG.Repository.Migrations
{
    [DbContext(typeof(Dbcontext))]
    partial class DbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryWeapon", b =>
                {
                    b.Property<int>("InventoriesId")
                        .HasColumnType("int");

                    b.Property<int>("WeaponsId")
                        .HasColumnType("int");

                    b.HasKey("InventoriesId", "WeaponsId");

                    b.HasIndex("WeaponsId");

                    b.ToTable("InventoryWeapon");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Armour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArmourModifier")
                        .HasColumnType("int");

                    b.Property<string>("ArmourTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AvailableToHero")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Armour");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Career", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CareerType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Career");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.EntityBaseSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("ArmourModifier")
                        .HasColumnType("int");

                    b.Property<int>("DamagerModifier")
                        .HasColumnType("int");

                    b.Property<int>("Energy")
                        .HasColumnType("int");

                    b.Property<int>("EnergyModifier")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("HealthModifier")
                        .HasColumnType("int");

                    b.Property<int>("Spirit")
                        .HasColumnType("int");

                    b.Property<int>("Stength")
                        .HasColumnType("int");

                    b.Property<int>("Vigor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EntityBaseSystem");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CareerId")
                        .HasColumnType("int");

                    b.Property<int?>("EntityBaseSystemId")
                        .HasColumnType("int");

                    b.Property<string>("HeroName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HeroXp")
                        .HasColumnType("int");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CareerId");

                    b.HasIndex("EntityBaseSystemId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("RaceId");

                    b.ToTable("Hero");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArmourId")
                        .HasColumnType("int");

                    b.Property<int>("Gold")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmourId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Monster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EntityBaseSystemId")
                        .HasColumnType("int");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("LevelDifficulty")
                        .HasColumnType("int");

                    b.Property<string>("MonsterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonsterXp")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntityBaseSystemId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Monster");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Potion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("PotionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("PotionTypeId");

                    b.ToTable("Potion");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.PotionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AvailableToHero")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PotionDice")
                        .HasColumnType("int");

                    b.Property<string>("PotionTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PotionType");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RaceType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Race");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.SkillRollType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SkillType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SkillRollType");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AvailableToHero")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillRoll")
                        .HasColumnType("int");

                    b.Property<bool>("StarterWeapon")
                        .HasColumnType("bit");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<int>("WeaponDamageModifier")
                        .HasColumnType("int");

                    b.Property<string>("WeaponName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeaponTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeaponTypeId");

                    b.ToTable("Weapon");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.WeaponType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DamageDice")
                        .HasColumnType("int");

                    b.Property<int>("EnergyCost")
                        .HasColumnType("int");

                    b.Property<int>("Range")
                        .HasColumnType("int");

                    b.Property<int>("SkillRollTypeId")
                        .HasColumnType("int");

                    b.Property<string>("WeaponTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SkillRollTypeId");

                    b.ToTable("WeaponType");
                });

            modelBuilder.Entity("InventoryWeapon", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.Inventory", null)
                        .WithMany()
                        .HasForeignKey("InventoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TextRPG.Repository.Models.Weapon", null)
                        .WithMany()
                        .HasForeignKey("WeaponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Hero", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.Career", "Career")
                        .WithMany()
                        .HasForeignKey("CareerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TextRPG.Repository.Models.EntityBaseSystem", "EntityBaseSystem")
                        .WithMany()
                        .HasForeignKey("EntityBaseSystemId");

                    b.HasOne("TextRPG.Repository.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId");

                    b.HasOne("TextRPG.Repository.Models.Race", "Race")
                        .WithMany()
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Career");

                    b.Navigation("EntityBaseSystem");

                    b.Navigation("Inventory");

                    b.Navigation("Race");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Inventory", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.Armour", "Armour")
                        .WithMany()
                        .HasForeignKey("ArmourId");

                    b.Navigation("Armour");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Monster", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.EntityBaseSystem", "EntityBaseSystem")
                        .WithMany()
                        .HasForeignKey("EntityBaseSystemId");

                    b.HasOne("TextRPG.Repository.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId");

                    b.Navigation("EntityBaseSystem");

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Potion", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.Inventory", null)
                        .WithMany("Potions")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TextRPG.Repository.Models.PotionType", "PotionType")
                        .WithMany()
                        .HasForeignKey("PotionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PotionType");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Weapon", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.WeaponType", "WeaponType")
                        .WithMany()
                        .HasForeignKey("WeaponTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeaponType");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.WeaponType", b =>
                {
                    b.HasOne("TextRPG.Repository.Models.SkillRollType", "SkillRollType")
                        .WithMany()
                        .HasForeignKey("SkillRollTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkillRollType");
                });

            modelBuilder.Entity("TextRPG.Repository.Models.Inventory", b =>
                {
                    b.Navigation("Potions");
                });
#pragma warning restore 612, 618
        }
    }
}
