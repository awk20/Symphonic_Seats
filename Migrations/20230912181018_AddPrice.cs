using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymphonicSeats2.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "CollectionItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CollectionItems");
        }
    }
}
