using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartPrice = table.Column<double>(type: "float", nullable: false),
                    CartQuantity = table.Column<int>(type: "int", nullable: false),
                    CartSubTotal = table.Column<double>(type: "float", nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartID);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartID", "CartName", "CartPrice", "CartQuantity", "CartSubTotal", "ImgURL" },
                values: new object[,]
                {
                    { 1, "Headphones", 10.0, 1, 10.0, "img/headphones.jpg" },
                    { 2, "Cell Phone Charger", 20.0, 1, 20.0, "img/headphones.jpg" },
                    { 3, "Earphones", 5.0, 1, 5.0, "img/earphones.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");
        }
    }
}
