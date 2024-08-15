using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class AddActualDollarsToSimService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Dollars",
                table: "SimServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 1,
                column: "Dollars",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 2,
                column: "Dollars",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 3,
                column: "Dollars",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 4,
                column: "Dollars",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dollars",
                table: "SimServices");
        }
    }
}
