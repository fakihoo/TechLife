using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class AddSimServiceTypeToSimService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SimServiceType",
                table: "SimServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 1,
                column: "SimServiceType",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 2,
                column: "SimServiceType",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 3,
                column: "SimServiceType",
                value: "Normal");

            migrationBuilder.UpdateData(
                table: "SimServices",
                keyColumn: "SimServiceId",
                keyValue: 4,
                column: "SimServiceType",
                value: "Normal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimServiceType",
                table: "SimServices");
        }
    }
}
