using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class AddSuppliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierItemId",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierItem",
                columns: table => new
                {
                    SupplierItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ShopStoreId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierItem", x => x.SupplierItemId);
                    table.ForeignKey(
                        name: "FK_SupplierItem_ShopStores_ShopStoreId",
                        column: x => x.ShopStoreId,
                        principalTable: "ShopStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierItem_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SupplierItemId",
                table: "Stocks",
                column: "SupplierItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierItem_ShopStoreId",
                table: "SupplierItem",
                column: "ShopStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierItem_SupplierId",
                table: "SupplierItem",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_SupplierItem_SupplierItemId",
                table: "Stocks",
                column: "SupplierItemId",
                principalTable: "SupplierItem",
                principalColumn: "SupplierItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_SupplierItem_SupplierItemId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "SupplierItem");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SupplierItemId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SupplierItemId",
                table: "Stocks");
        }
    }
}
