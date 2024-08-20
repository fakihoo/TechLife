using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSimService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "SimServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 1,
                column: "Viewed",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 2,
                column: "Viewed",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 3,
                column: "Viewed",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 4,
                column: "Viewed",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "SimServices");
        }
    }
}
