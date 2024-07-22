﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Repository.Data;

#nullable disable

namespace Store.Repository.Data.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Store.Core.Entities.Good", b =>
                {
                    b.Property<string>("GoodID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GoodInitialBalance")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GoodID");

                    b.HasIndex("StoreName");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("Store.Core.Entities.StoreInfo", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("StoreFileDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Name");

                    b.ToTable("StoresInfo");
                });

            modelBuilder.Entity("Store.Core.Entities.Transaction", b =>
                {
                    b.Property<string>("TransactionID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoodID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionID");

                    b.HasIndex("GoodID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Store.Core.Entities.Good", b =>
                {
                    b.HasOne("Store.Core.Entities.StoreInfo", "Store")
                        .WithMany("Goods")
                        .HasForeignKey("StoreName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Store.Core.Entities.Transaction", b =>
                {
                    b.HasOne("Store.Core.Entities.Good", "Good")
                        .WithMany("Transactions")
                        .HasForeignKey("GoodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Good");
                });

            modelBuilder.Entity("Store.Core.Entities.Good", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Store.Core.Entities.StoreInfo", b =>
                {
                    b.Navigation("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
