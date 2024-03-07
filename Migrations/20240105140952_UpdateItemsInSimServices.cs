using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemsInSimServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "SimServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "SimServices",
                columns: new[] { "SimServiceId", "Amount", "ImgUrl", "PhoneNumber", "Price", "SimServiceName", "SimType" },
                values: new object[,]
                {
                    { 1, 0, "~/img/4.5USDRecharge Voucher.jpg", " ", 4m, "4.5$ Recharging Card", "Mtc" },
                    { 2, 0, "~/img/1657112223214_image.jpg", " ", 7m, "7.5$ Recharging Card", "Mtc" },
                    { 3, 0, "~/img/Untitled-6.jpg", " ", 4m, "4.5$ Recharging Card", "Alfa" },
                    { 4, 0, "~/img/1fd2fc9d-e1ac-4c1e-9c07-1d78f99055cb.jpg", " ", 7m, "7.5$ Recharging Card", "Alfa" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "SimServices");
        }
    }
}
