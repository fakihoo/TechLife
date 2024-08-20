using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class AddViewToSimServiceToDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "SimServiceToDos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "SimServiceToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "SimServiceToDos");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "SimServiceToDos");
        }
    }
}
