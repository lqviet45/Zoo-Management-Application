﻿// <auto-generated />
using System;
using Entities.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230925133518_UpdateExp")]
    partial class UpdateExp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimalMeal", b =>
                {
                    b.Property<long>("AnimalId")
                        .HasColumnType("bigint");

                    b.Property<long>("MealsMealId")
                        .HasColumnType("bigint");

                    b.HasKey("AnimalId", "MealsMealId");

                    b.HasIndex("MealsMealId");

                    b.ToTable("AnimalMeal");
                });

            modelBuilder.Entity("AnimalUser", b =>
                {
                    b.Property<long>("AnimalsAnimalId")
                        .HasColumnType("bigint");

                    b.Property<long>("ZooTrainersUserId")
                        .HasColumnType("bigint");

                    b.HasKey("AnimalsAnimalId", "ZooTrainersUserId");

                    b.HasIndex("ZooTrainersUserId");

                    b.ToTable("AnimalUser");
                });

            modelBuilder.Entity("Entities.Models.Animal", b =>
                {
                    b.Property<long>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AnimalId"));

                    b.Property<string>("AnimalName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("CageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateArrive")
                        .HasColumnType("DateTime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("ZooTrainerId")
                        .HasColumnType("bigint");

                    b.HasKey("AnimalId");

                    b.HasIndex("CageId");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Animal", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AreaId"));

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.HasKey("AreaId");

                    b.ToTable("Area", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Cage", b =>
                {
                    b.Property<int>("CageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CageId"));

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("CageName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.HasKey("CageId");

                    b.HasIndex("AreaId");

                    b.ToTable("Cage", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Custommer", b =>
                {
                    b.Property<long>("CustommerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CustommerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CustommerId");

                    b.ToTable("Custommer", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Experience", b =>
                {
                    b.Property<int>("ExperienceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExperienceId"));

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ExperienceId");

                    b.HasIndex("UserId");

                    b.ToTable("Experience", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Meal", b =>
                {
                    b.Property<long>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MealId"));

                    b.Property<long>("AnimalId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FeedingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MealId");

                    b.ToTable("Meal", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderId"));

                    b.Property<long>("CustommerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustommerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Entities.Models.OrderDetail", b =>
                {
                    b.Property<long>("OrderID")
                        .HasColumnType("bigint");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderID", "TicketId");

                    b.HasIndex("TicketId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SkillId"));

                    b.Property<int>("ExperienceId")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("SkillId");

                    b.HasIndex("ExperienceId");

                    b.ToTable("Skill", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Species", b =>
                {
                    b.Property<int>("SpeciesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpeciesId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpeciesName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SpeciesId");

                    b.ToTable("Species", (string)null);
                });

            modelBuilder.Entity("Entities.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("TicketName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("TicketId");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DateTime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("AnimalMeal", b =>
                {
                    b.HasOne("Entities.Models.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Meal", null)
                        .WithMany()
                        .HasForeignKey("MealsMealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalUser", b =>
                {
                    b.HasOne("Entities.Models.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsAnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("ZooTrainersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.Animal", b =>
                {
                    b.HasOne("Entities.Models.Cage", "Cage")
                        .WithMany()
                        .HasForeignKey("CageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Species", "Species")
                        .WithMany()
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cage");

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Entities.Models.Cage", b =>
                {
                    b.HasOne("Entities.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Entities.Models.Experience", b =>
                {
                    b.HasOne("Entities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Models.Order", b =>
                {
                    b.HasOne("Entities.Models.Custommer", "Custommer")
                        .WithMany()
                        .HasForeignKey("CustommerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Custommer");
                });

            modelBuilder.Entity("Entities.Models.OrderDetail", b =>
                {
                    b.HasOne("Entities.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Entities.Models.Skill", b =>
                {
                    b.HasOne("Entities.Models.Experience", "Experience")
                        .WithMany("Skills")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experience");
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.HasOne("Entities.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Entities.Models.Experience", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}