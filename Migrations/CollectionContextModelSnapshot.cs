﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SymphonicSeats2.Models;

#nullable disable

namespace SymphonicSeats2.Migrations
{
    [DbContext(typeof(CollectionContext))]
    partial class CollectionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("SymphonicSeats2.Models.CollectionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ConcertTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageURL")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Votes")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CollectionItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcertTime = new DateTime(2023, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "See metal legends Metallica in concert in San Francisco.",
                            ImageURL = "https://cdn-aeoeg.nitrocdn.com/CEyMkTCvQqLzCiakJhTISKVqIxcNYCml/assets/images/optimized/rev-9a82380/secureyourtrademark.com/wp-content/uploads/2022/07/img-metallica-trademarks.jpg",
                            Location = "San Francisco, California",
                            Name = "Metallica",
                            Votes = 0
                        },
                        new
                        {
                            Id = 2,
                            ConcertTime = new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Weezer is back on tour performing their latest album SZNZ.",
                            ImageURL = "https://s3.amazonaws.com/heights-photos/wp-content/uploads/2019/03/03130519/Weezer.jpg",
                            Location = "Austin, Texas",
                            Name = "Weezer",
                            Votes = 0
                        },
                        new
                        {
                            Id = 3,
                            ConcertTime = new DateTime(2023, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Kings of the alternative genre, The Cure are back on tour for the first time in years.",
                            ImageURL = "https://blog.roughtrade.com/content/images/size/w1000/2022/02/Screen-Shot-2022-02-14-at-9.21.39-AM.png",
                            Location = "Miami, Florida",
                            Name = "The Cure",
                            Votes = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
