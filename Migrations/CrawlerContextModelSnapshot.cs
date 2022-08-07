﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NovaVida.DataContext;

#nullable disable

namespace NovaVida.Migrations
{
    [DbContext(typeof(CrawlerContext))]
    partial class CrawlerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NovaVida.Models.ProductReviews", b =>
                {
                    b.Property<int>("IDProductReviews")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDProductReviews"), 1L, 1);

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDProduct")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("register")
                        .HasColumnType("datetime2");

                    b.Property<string>("review")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDProductReviews");

                    b.ToTable("ProductReviews");
                });

            modelBuilder.Entity("NovaVida.Models.Products", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProduct"), 1L, 1);

                    b.Property<string>("NameProduct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PriceProduct")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("URLProduct")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProduct");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
