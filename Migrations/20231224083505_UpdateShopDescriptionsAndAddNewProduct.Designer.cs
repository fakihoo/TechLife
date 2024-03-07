﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechLife.Data;

#nullable disable

namespace TechLife.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231224083505_UpdateShopDescriptionsAndAddNewProduct")]
    partial class UpdateShopDescriptionsAndAddNewProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechLife.Models.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ShopId");

                    b.ToTable("Shops");

                    b.HasData(
                        new
                        {
                            ShopId = 1,
                            Category = "ACCESSORIES",
                            Description = "Crisp, immersive sound experience. Choose from over-ear, on-ear, or in-ear styles for personalized comfort. Features include wireless options, noise cancellation, and convenient controls. Elevate your audio journey with our headphones.",
                            ImgURL = "img/headphones.jpg",
                            Name = "Headphones",
                            Price = 45,
                            Quantity = 0
                        },
                        new
                        {
                            ShopId = 2,
                            Category = "ACCESSORIES",
                            Description = "Reliable power solutions for your devices. Fast-charging and durable phone chargers to keep you connected on the go. Choose from a variety of models tailored to your device's needs. Stay powered up, wherever you are",
                            ImgURL = "img/cellphone-charger.jpg",
                            Name = "Cell Phone Charger",
                            Price = 10,
                            Quantity = 0
                        },
                        new
                        {
                            ShopId = 3,
                            Category = "ACCESSORIES",
                            Description = "Compact and powerful earphones for exceptional sound on the move. Enjoy immersive audio with comfortable in-ear designs. Choose from wired or wireless options with superior sound quality. Your perfect companion for music, calls, and more",
                            ImgURL = "img/earphones.jpg",
                            Name = "Earphones",
                            Price = 5,
                            Quantity = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
