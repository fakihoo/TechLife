using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "Category", "Description", "ImgURL", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "ACCESSORIES", "The BEst Price In town", "img/headphones.jpg", "Headphones", 45, 0 },
                    { 2, "ACCESSORIES", "The Best Price In town, sdfsdsfds", "img/cellphone-charger.jpg", "Cell Phone Charger", 10, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 2);
        }
    }
}
