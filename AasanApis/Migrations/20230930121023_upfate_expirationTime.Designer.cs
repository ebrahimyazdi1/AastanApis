﻿// <auto-generated />
using System;
using AasanApis.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace AasanApis.Migrations
{
    [DbContext(typeof(AastanDbContext))]
    [Migration("20230930121023_upfate_expirationTime")]
    partial class upfate_expirationTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AasanApis.Data.Entities.AastanReqLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("JsonReq")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("PublicAppId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("PublicReqId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ServiceId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Aastan_LOG_REQ", (string)null);
                });

            modelBuilder.Entity("AasanApis.Data.Entities.AastanResLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("HTTPStatusCode")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("JsonRes")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("PublicReqId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("ReqLogId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("ResCode")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.HasIndex("ReqLogId")
                        .IsUnique();

                    b.ToTable("Aastan_LOG_RES", (string)null);
                });

            modelBuilder.Entity("AasanApis.Data.Entities.AccessTokenEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("accessTocken");

                    b.Property<DateTime>("TokenDateTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("TockenDateTime");

                    b.Property<string>("TokenName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("NAJI_ACCESS_TOCKEN", (string)null);
                });

            modelBuilder.Entity("AasanApis.Data.Entities.ShahkarRequestsLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("ExpirationDateTime")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<long>("ExpireTimeInSecond")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("RequestId")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("SafeServiceId")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("TokenType")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("Aastan_ShahkarRequestsLog", (string)null);
                });

            modelBuilder.Entity("AasanApis.Data.Entities.AastanResLog", b =>
                {
                    b.HasOne("AasanApis.Data.Entities.AastanReqLog", "ReqLog")
                        .WithMany("AastanResLogs")
                        .HasForeignKey("ReqLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReqLog");
                });

            modelBuilder.Entity("AasanApis.Data.Entities.AastanReqLog", b =>
                {
                    b.Navigation("AastanResLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
