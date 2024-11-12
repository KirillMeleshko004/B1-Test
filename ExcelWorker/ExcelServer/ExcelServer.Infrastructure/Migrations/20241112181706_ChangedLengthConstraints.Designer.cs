﻿// <auto-generated />
using System;
using ExcelServer.Infrastructure.DB.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExcelServer.Infrastructure.Migrations
{
    [DbContext(typeof(ExcelWorkerDbContext))]
    [Migration("20241112181706_ChangedLengthConstraints")]
    partial class ChangedLengthConstraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExcelServer.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ClosingBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("ClosingBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("OpeningBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("OpeningBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<Guid>("SummaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TurnoverCredit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.HasKey("Id");

                    b.HasIndex("SummaryId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.AccountsSummary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ClosingBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("ClosingBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("OpeningBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("OpeningBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<Guid>("SummaryClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TurnoverCredit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.HasKey("Id");

                    b.HasIndex("SummaryClassId");

                    b.ToTable("AccountsSummary", (string)null);
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.SummaryClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ClosingBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("ClosingBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("OpeningBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("OpeningBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<decimal>("TurnoverCredit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<Guid>("TurnoverDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TurnoverDocumentId");

                    b.ToTable("SummaryClass", (string)null);
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.TurnoverDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<decimal>("ClosingBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("ClosingBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OpeningBalanceAsset")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("OpeningBalanceLiability")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<decimal>("TurnoverCredit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasPrecision(30, 2)
                        .HasColumnType("decimal(30,2)");

                    b.HasKey("Id");

                    b.ToTable("TurnoverDocument", (string)null);
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.Account", b =>
                {
                    b.HasOne("ExcelServer.Domain.Entities.AccountsSummary", null)
                        .WithMany("Accounts")
                        .HasForeignKey("SummaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.AccountsSummary", b =>
                {
                    b.HasOne("ExcelServer.Domain.Entities.SummaryClass", null)
                        .WithMany("AccountSummaries")
                        .HasForeignKey("SummaryClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.SummaryClass", b =>
                {
                    b.HasOne("ExcelServer.Domain.Entities.TurnoverDocument", null)
                        .WithMany("SummaryClasses")
                        .HasForeignKey("TurnoverDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.AccountsSummary", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.SummaryClass", b =>
                {
                    b.Navigation("AccountSummaries");
                });

            modelBuilder.Entity("ExcelServer.Domain.Entities.TurnoverDocument", b =>
                {
                    b.Navigation("SummaryClasses");
                });
#pragma warning restore 612, 618
        }
    }
}
