﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NinjaWikiAPI.Data;

#nullable disable

namespace NinjaWikiAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231212074209_nullNinjaRelations")]
    partial class nullNinjaRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NinjaWikiAPI.Models.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VillageId");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Clan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clans");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Ninja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClanId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTraitor")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RankId")
                        .HasColumnType("int");

                    b.Property<int?>("VillageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClanId");

                    b.HasIndex("RankId");

                    b.HasIndex("VillageId");

                    b.ToTable("Ninjas");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.NinjaBattles", b =>
                {
                    b.Property<int>("NinjaId")
                        .HasColumnType("int");

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.HasKey("NinjaId", "BattleId");

                    b.HasIndex("BattleId");

                    b.ToTable("NinjaBattles");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RankId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RankId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.SkillsNinja", b =>
                {
                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<int>("NinjaId")
                        .HasColumnType("int");

                    b.HasKey("SkillId", "NinjaId");

                    b.HasIndex("NinjaId");

                    b.ToTable("SkillsNinja");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Village", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Villages");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Battle", b =>
                {
                    b.HasOne("NinjaWikiAPI.Models.Village", null)
                        .WithMany("Battles")
                        .HasForeignKey("VillageId");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Ninja", b =>
                {
                    b.HasOne("NinjaWikiAPI.Models.Clan", "Clan")
                        .WithMany("Ninjas")
                        .HasForeignKey("ClanId");

                    b.HasOne("NinjaWikiAPI.Models.Rank", "Rank")
                        .WithMany("Ninjas")
                        .HasForeignKey("RankId");

                    b.HasOne("NinjaWikiAPI.Models.Village", "Village")
                        .WithMany("Ninjas")
                        .HasForeignKey("VillageId");

                    b.Navigation("Clan");

                    b.Navigation("Rank");

                    b.Navigation("Village");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.NinjaBattles", b =>
                {
                    b.HasOne("NinjaWikiAPI.Models.Ninja", "Ninja")
                        .WithMany("NinjaBattles")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NinjaWikiAPI.Models.Battle", "Battle")
                        .WithMany("NinjaBattles")
                        .HasForeignKey("NinjaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Battle");

                    b.Navigation("Ninja");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Skill", b =>
                {
                    b.HasOne("NinjaWikiAPI.Models.Category", "Category")
                        .WithMany("Skills")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NinjaWikiAPI.Models.Rank", "Rank")
                        .WithMany("Skills")
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.SkillsNinja", b =>
                {
                    b.HasOne("NinjaWikiAPI.Models.Skill", "Skill")
                        .WithMany("SkillsNinja")
                        .HasForeignKey("NinjaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NinjaWikiAPI.Models.Ninja", "Ninja")
                        .WithMany("SkillsNinja")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ninja");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Battle", b =>
                {
                    b.Navigation("NinjaBattles");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Category", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Clan", b =>
                {
                    b.Navigation("Ninjas");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Ninja", b =>
                {
                    b.Navigation("NinjaBattles");

                    b.Navigation("SkillsNinja");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Rank", b =>
                {
                    b.Navigation("Ninjas");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Skill", b =>
                {
                    b.Navigation("SkillsNinja");
                });

            modelBuilder.Entity("NinjaWikiAPI.Models.Village", b =>
                {
                    b.Navigation("Battles");

                    b.Navigation("Ninjas");
                });
#pragma warning restore 612, 618
        }
    }
}