using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymphonicSeats2.Migrations
{
    /// <inheritdoc />
    public partial class AddNumTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumTickets",
                table: "CollectionItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NumTickets", "Price" },
                values: new object[] { 0, 350L });

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NumTickets", "Price" },
                values: new object[] { 0, 200L });

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NumTickets", "Price" },
                values: new object[] { 0, 175L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumTickets",
                table: "CollectionItems");

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0L);

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 0L);
        }
    }
}
