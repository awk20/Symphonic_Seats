using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SymphonicSeats2.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CollectionItems",
                columns: new[] { "Id", "ConcertTime", "Description", "ImageURL", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "See metal legends Metallica in concert in San Francisco.", "https://cdn-aeoeg.nitrocdn.com/CEyMkTCvQqLzCiakJhTISKVqIxcNYCml/assets/images/optimized/rev-9a82380/secureyourtrademark.com/wp-content/uploads/2022/07/img-metallica-trademarks.jpg", "San Francisco, California", "Metallica" },
                    { 2, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Weezer is back on tour performing their latest album SZNZ.", "https://s3.amazonaws.com/heights-photos/wp-content/uploads/2019/03/03130519/Weezer.jpg", "Austin, Texas", "Weezer" },
                    { 3, new DateTime(2023, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kings of the alternative genre, The Cure are back on tour for the first time in years.", "https://blog.roughtrade.com/content/images/size/w1000/2022/02/Screen-Shot-2022-02-14-at-9.21.39-AM.png", "Miami, Florida", "The Cure" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
