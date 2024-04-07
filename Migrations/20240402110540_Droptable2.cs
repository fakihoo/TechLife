using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class Droptable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "Orders"); // Then drop Orders table
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Implement the logic to recreate the dropped tables in the Down method.
            // Note: This is optional and depends on your requirements.
        }
    }
}
