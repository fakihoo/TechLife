using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class RenameSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_SupplierItem_SupplierItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItem_ShopStores_ShopStoreId",
                table: "SupplierItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItem_Supplier_SupplierId",
                table: "SupplierItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierItem",
                table: "SupplierItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.RenameTable(
                name: "SupplierItem",
                newName: "SupplierItems");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItem_SupplierId",
                table: "SupplierItems",
                newName: "IX_SupplierItems_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItem_ShopStoreId",
                table: "SupplierItems",
                newName: "IX_SupplierItems_ShopStoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierItems",
                table: "SupplierItems",
                column: "SupplierItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_SupplierItems_SupplierItemId",
                table: "Stocks",
                column: "SupplierItemId",
                principalTable: "SupplierItems",
                principalColumn: "SupplierItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItems_ShopStores_ShopStoreId",
                table: "SupplierItems",
                column: "ShopStoreId",
                principalTable: "ShopStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItems_Suppliers_SupplierId",
                table: "SupplierItems",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_SupplierItems_SupplierItemId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItems_ShopStores_ShopStoreId",
                table: "SupplierItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierItems_Suppliers_SupplierId",
                table: "SupplierItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierItems",
                table: "SupplierItems");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "SupplierItems",
                newName: "SupplierItem");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItems_SupplierId",
                table: "SupplierItem",
                newName: "IX_SupplierItem_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierItems_ShopStoreId",
                table: "SupplierItem",
                newName: "IX_SupplierItem_ShopStoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierItem",
                table: "SupplierItem",
                column: "SupplierItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_SupplierItem_SupplierItemId",
                table: "Stocks",
                column: "SupplierItemId",
                principalTable: "SupplierItem",
                principalColumn: "SupplierItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItem_ShopStores_ShopStoreId",
                table: "SupplierItem",
                column: "ShopStoreId",
                principalTable: "ShopStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierItem_Supplier_SupplierId",
                table: "SupplierItem",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
