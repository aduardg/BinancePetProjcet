﻿// <auto-generated />
using BinanceJob.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BinanceJob.Migrations
{
    [DbContext(typeof(DbTransaction))]
    partial class DbTransactionModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BinanceJob.Models.TradeElement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("checkColumn")
                        .HasColumnType("text");

                    b.Property<bool>("isBestMatch")
                        .HasColumnType("boolean");

                    b.Property<bool>("isBuyerMaker")
                        .HasColumnType("boolean");

                    b.Property<string>("namePart")
                        .HasColumnType("text");

                    b.Property<string>("price")
                        .HasColumnType("text");

                    b.Property<string>("qty")
                        .HasColumnType("text");

                    b.Property<string>("quoteQty")
                        .HasColumnType("text");

                    b.Property<long>("time")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("TradeElements", "Binance");
                });

            modelBuilder.Entity("BinanceJob.Models.ValueNames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isBlocked")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("ValueNames", "Binance");
                });
#pragma warning restore 612, 618
        }
    }
}
