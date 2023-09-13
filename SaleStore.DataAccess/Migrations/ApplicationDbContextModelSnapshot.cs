﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SaleStore.Data;

#nullable disable

namespace SaleStore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SaleStore.Model.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            ID = 2,
                            DisplayOrder = 2,
                            Name = "SciFi"
                        },
                        new
                        {
                            ID = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("SaleStore.Model.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ListPrice")
                        .HasColumnType("double precision");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<double>("Price100")
                        .HasColumnType("double precision");

                    b.Property<double>("Price50")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Author = "John Smith",
                            CategoryID = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                            ISBN = "ABCDEF1234",
                            ImageUrl = "",
                            ListPrice = 100.0,
                            Price = 80.0,
                            Price100 = 60.0,
                            Price50 = 65.0,
                            Title = "Lorem"
                        },
                        new
                        {
                            ID = 2,
                            Author = "Sarah Johnson",
                            CategoryID = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod.",
                            ISBN = "ZXCVB0987Y",
                            ImageUrl = "",
                            ListPrice = 120.0,
                            Price = 90.0,
                            Price100 = 70.0,
                            Price50 = 75.0,
                            Title = "Ipsum"
                        },
                        new
                        {
                            ID = 3,
                            Author = "David Brown",
                            CategoryID = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor.",
                            ISBN = "QWERT5678P",
                            ImageUrl = "",
                            ListPrice = 80.0,
                            Price = 60.0,
                            Price100 = 52.0,
                            Price50 = 55.0,
                            Title = "Dolor"
                        },
                        new
                        {
                            ID = 4,
                            Author = "Emily Davis",
                            CategoryID = 2,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed.",
                            ISBN = "ASDFG1234H",
                            ImageUrl = "",
                            ListPrice = 140.0,
                            Price = 110.0,
                            Price100 = 95.0,
                            Price50 = 100.0,
                            Title = "Sit"
                        },
                        new
                        {
                            ID = 5,
                            Author = "Michael Wilson",
                            CategoryID = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
                            ISBN = "JKLPO6789I",
                            ImageUrl = "",
                            ListPrice = 70.0,
                            Price = 55.0,
                            Price100 = 48.0,
                            Price50 = 50.0,
                            Title = "Amet"
                        },
                        new
                        {
                            ID = 6,
                            Author = "Jessica Thompson",
                            CategoryID = 3,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                            ISBN = "MNBVC4321U",
                            ImageUrl = "",
                            ListPrice = 130.0,
                            Price = 100.0,
                            Price100 = 85.0,
                            Price50 = 90.0,
                            Title = "Consectetur"
                        });
                });

            modelBuilder.Entity("SaleStore.Model.Product", b =>
                {
                    b.HasOne("SaleStore.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
