﻿// <auto-generated />
using System;
using Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Presentation.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20231214115236_AddDataSeed")]
    partial class AddDataSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kaşar",
                            Url = "kasar"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bal",
                            Url = "bal"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Peynir",
                            Url = "peynir"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Eski Kaşar",
                            Url = "eski-kasar"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Yeni Kaşar",
                            Url = "yeni-kasar"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Kara Kovan Bal",
                            Url = "kara-kovan-bal"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Petek Bal",
                            Url = "petek-bal"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Süzme Bal",
                            Url = "suzme-bal"
                        });
                });

            modelBuilder.Entity("Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHome")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateAdded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Eski Kars Kaşarı Açıklaması",
                            ImageUrl = "1.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Eski Kars Kaşarı",
                            Price = 300.0,
                            StockQuantity = 100,
                            Url = "eksi-kars-kasari",
                            Weight = 1000.0
                        },
                        new
                        {
                            Id = 2,
                            DateAdded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Yeni Kars Kaşarı Açıklaması",
                            ImageUrl = "2.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Yeni Kars Kaşarı",
                            Price = 280.0,
                            StockQuantity = 100,
                            Url = "yeni-kars-kasari",
                            Weight = 1000.0
                        },
                        new
                        {
                            Id = 3,
                            DateAdded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Natural Beyaz Peynir Açıklaması",
                            ImageUrl = "3.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Natural Beyaz Peynir",
                            Price = 250.0,
                            StockQuantity = 100,
                            Url = "natural-beyaz-peynir",
                            Weight = 1000.0
                        },
                        new
                        {
                            Id = 4,
                            DateAdded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Kara Kovan Petek Bal Açıklaması",
                            ImageUrl = "4.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Kara Kovan Petek Bal",
                            Price = 550.0,
                            StockQuantity = 100,
                            Url = "kara-kovan-petek-bal",
                            Weight = 1200.0
                        },
                        new
                        {
                            Id = 5,
                            DateAdded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Süzme Bal Açıklaması",
                            ImageUrl = "5.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Süzme Bal",
                            Price = 500.0,
                            StockQuantity = 100,
                            Url = "suzme-bal",
                            Weight = 1000.0
                        },
                        new
                        {
                            Id = 6,
                            DateAdded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Çeçil Peyniri Açıklaması",
                            ImageUrl = "6.jpg",
                            IsApproved = true,
                            IsHome = true,
                            Name = "Çeçil Peyniri",
                            Price = 260.0,
                            StockQuantity = 100,
                            Url = "cecil-peyniri",
                            Weight = 1000.0
                        });
                });

            modelBuilder.Entity("Entity.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 1,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 5
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 6
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 7
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 2
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 8
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3
                        });
                });

            modelBuilder.Entity("Entity.ProductCategory", b =>
                {
                    b.HasOne("Entity.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entity.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Entity.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}