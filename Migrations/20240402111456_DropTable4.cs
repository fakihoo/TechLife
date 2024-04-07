using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class DropTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCarts");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // If you want to be able to revert the changes, 
            // implement the logic to recreate the tables in the Down method.
            // Note: Implementing Down method is optional.
        }
    }
}
