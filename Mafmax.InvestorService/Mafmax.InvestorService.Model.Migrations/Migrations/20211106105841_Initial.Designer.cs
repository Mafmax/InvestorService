﻿// <auto-generated />
using System;
using Mafmax.InvestorService.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mafmax.InvestorService.Model.Migrations.Migrations
{
    [DbContext(typeof(InvestorDbContext))]
    [Migration("20211106105841_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Assets.AssetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BaseCurrency");

                    b.Property<string>("Isin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IssuerId")
                        .HasColumnType("int");

                    b.Property<int>("LotSize")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StockId")
                        .HasColumnType("int");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IssuerId");

                    b.HasIndex("StockId");

                    b.ToTable("Assets");

                    b.HasDiscriminator<string>("Class").HasValue("AssetEntity");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.CountryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.ExchangeTransaction.ExchangeTransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssetId")
                        .HasColumnType("int");

                    b.Property<int?>("InvestmentPortfolioEntityId")
                        .HasColumnType("int");

                    b.Property<int>("LotsCount")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,6)");

                    b.Property<int?>("StockExchangeId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("InvestmentPortfolioEntityId");

                    b.HasIndex("StockExchangeId");

                    b.ToTable("ExchangeTransactions");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.IndustryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.InvestmentPortfolioEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InvestorEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("InvestorEntityId");

                    b.ToTable("InvestmentPortfolios");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.IssuerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<int?>("IndustryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("IndustryId");

                    b.ToTable("Issuers");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.StockExchangeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StockExchanges");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Users.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserEntity");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Assets.BondEntity", b =>
                {
                    b.HasBaseType("Mafmax.InvestorService.Model.Entities.Assets.AssetEntity");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.ToTable("Assets");

                    b.HasDiscriminator().HasValue("Облигация");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Assets.ShareEntity", b =>
                {
                    b.HasBaseType("Mafmax.InvestorService.Model.Entities.Assets.AssetEntity");

                    b.Property<bool>("IsPreferred")
                        .HasColumnType("bit");

                    b.ToTable("Assets");

                    b.HasDiscriminator().HasValue("Акция");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Users.InvestorEntity", b =>
                {
                    b.HasBaseType("Mafmax.InvestorService.Model.Entities.Users.UserEntity");

                    b.HasDiscriminator().HasValue("InvestorEntity");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Assets.AssetEntity", b =>
                {
                    b.HasOne("Mafmax.InvestorService.Model.Entities.IssuerEntity", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId");

                    b.HasOne("Mafmax.InvestorService.Model.Entities.StockExchangeEntity", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId");

                    b.OwnsOne("Mafmax.InvestorService.Model.Entities.CirculationPeriodEntity", "Circulation", b1 =>
                        {
                            b1.Property<int>("AssetEntityId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime?>("End")
                                .HasColumnType("datetime2")
                                .HasColumnName("EndCirculation");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2")
                                .HasColumnName("StartCirculation");

                            b1.HasKey("AssetEntityId");

                            b1.ToTable("Assets");

                            b1.WithOwner()
                                .HasForeignKey("AssetEntityId");
                        });

                    b.Navigation("Circulation");

                    b.Navigation("Issuer");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.ExchangeTransaction.ExchangeTransactionEntity", b =>
                {
                    b.HasOne("Mafmax.InvestorService.Model.Entities.Assets.AssetEntity", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("Mafmax.InvestorService.Model.Entities.InvestmentPortfolioEntity", null)
                        .WithMany("Transactions")
                        .HasForeignKey("InvestmentPortfolioEntityId");

                    b.HasOne("Mafmax.InvestorService.Model.Entities.StockExchangeEntity", "StockExchange")
                        .WithMany()
                        .HasForeignKey("StockExchangeId");

                    b.Navigation("Asset");

                    b.Navigation("StockExchange");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.InvestmentPortfolioEntity", b =>
                {
                    b.HasOne("Mafmax.InvestorService.Model.Entities.Users.InvestorEntity", null)
                        .WithMany("Portfolios")
                        .HasForeignKey("InvestorEntityId");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.IssuerEntity", b =>
                {
                    b.HasOne("Mafmax.InvestorService.Model.Entities.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Mafmax.InvestorService.Model.Entities.IndustryEntity", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId");

                    b.Navigation("Country");

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.InvestmentPortfolioEntity", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Mafmax.InvestorService.Model.Entities.Users.InvestorEntity", b =>
                {
                    b.Navigation("Portfolios");
                });
#pragma warning restore 612, 618
        }
    }
}
