﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mrp2.Data;

#nullable disable

namespace Mrp2.Migrations.DataContextEFMigrations
{
    [DbContext(typeof(DataContextEF))]
    [Migration("20240727094448_x1")]
    partial class x1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mrp2.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Mrp2.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("MaterialsReserved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OrderCompleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("OrderTotalCost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Po")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("RawMaterialCost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("RawMaterialIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RecipeCost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Mrp2.Models.OrderRawMaterial", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("RawMaterialId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("OrderId", "RawMaterialId");

                    b.HasIndex("RawMaterialId");

                    b.ToTable("OrderRawMaterials");
                });

            modelBuilder.Entity("Mrp2.Models.RawMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BoundQTY")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Eoq")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeadTime")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("ReorderPoint")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("SafetyStock")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.ToTable("RawMaterial", (string)null);
                });

            modelBuilder.Entity("Mrp2.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("Mrp2.Models.RecipeRawMaterial", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("RawMaterialId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("RecipeId", "RawMaterialId");

                    b.HasIndex("RawMaterialId");

                    b.ToTable("recipeRawMaterials");
                });

            modelBuilder.Entity("Mrp2.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("Mrp2.Models.Suppliers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers", (string)null);
                });

            modelBuilder.Entity("Mrp2.Models.Units", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Units", (string)null);
                });

            modelBuilder.Entity("Mrp2.Models.Order", b =>
                {
                    b.HasOne("Mrp2.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Mrp2.Models.OrderRawMaterial", b =>
                {
                    b.HasOne("Mrp2.Models.Order", "Order")
                        .WithMany("OrderRawMaterials")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mrp2.Models.RawMaterial", "RawMaterial")
                        .WithMany("OrderRawMaterials")
                        .HasForeignKey("RawMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("RawMaterial");
                });

            modelBuilder.Entity("Mrp2.Models.RecipeRawMaterial", b =>
                {
                    b.HasOne("Mrp2.Models.RawMaterial", "RawMaterial")
                        .WithMany("RecipeRawMaterials")
                        .HasForeignKey("RawMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mrp2.Models.Recipe", "Recipe")
                        .WithMany("recipeRawMaterials")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RawMaterial");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Mrp2.Models.Order", b =>
                {
                    b.Navigation("OrderRawMaterials");
                });

            modelBuilder.Entity("Mrp2.Models.RawMaterial", b =>
                {
                    b.Navigation("OrderRawMaterials");

                    b.Navigation("RecipeRawMaterials");
                });

            modelBuilder.Entity("Mrp2.Models.Recipe", b =>
                {
                    b.Navigation("recipeRawMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}
