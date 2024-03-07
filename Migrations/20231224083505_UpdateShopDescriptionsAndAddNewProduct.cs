using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShopDescriptionsAndAddNewProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 1,
                column: "Description",
                value: "Crisp, immersive sound experience. Choose from over-ear, on-ear, or in-ear styles for personalized comfort. Features include wireless options, noise cancellation, and convenient controls. Elevate your audio journey with our headphones.");

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 2,
                column: "Description",
                value: "Reliable power solutions for your devices. Fast-charging and durable phone chargers to keep you connected on the go. Choose from a variety of models tailored to your device's needs. Stay powered up, wherever you are");

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "Category", "Description", "ImgURL", "Name", "Price", "Quantity" },
                values: new object[] { 3, "ACCESSORIES", "Compact and powerful earphones for exceptional sound on the move. Enjoy immersive audio with comfortable in-ear designs. Choose from wired or wireless options with superior sound quality. Your perfect companion for music, calls, and more", "img/earphones.jpg", "Earphones", 5, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 1,
                column: "Description",
                value: "The BEst Price In town");

            migrationBuilder.UpdateData(
                table: "Shops",
                keyColumn: "ShopId",
                keyValue: 2,
                column: "Description",
                value: "The Best Price In town, sdfsdsfds");
        }
    }
}
