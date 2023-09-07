using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymphonicSeats2.Migrations
{
    /// <inheritdoc />
    public partial class introduceVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "CollectionItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "Votes",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "Votes",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CollectionItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "Votes",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "CollectionItems");
        }
    }
}
