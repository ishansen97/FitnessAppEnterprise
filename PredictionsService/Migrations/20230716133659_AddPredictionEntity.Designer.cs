﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PredictionsService.Context;

namespace PredictionsService.Migrations
{
    [DbContext(typeof(PredictionDbContext))]
    [Migration("20230716133659_AddPredictionEntity")]
    partial class AddPredictionEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PredictionsService.Entity.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BMI")
                        .HasColumnType("float");

                    b.Property<double>("BodyFatPercentage")
                        .HasColumnType("float");

                    b.Property<double>("CurrentWeight")
                        .HasColumnType("float");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PredictedBMI")
                        .HasColumnType("float");

                    b.Property<DateTime>("PredictedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("PredictedWeight")
                        .HasColumnType("float");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeightStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Predictions");
                });

            modelBuilder.Entity("PredictionsService.Entity.PredictionConstant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PredictionConstants");
                });
#pragma warning restore 612, 618
        }
    }
}