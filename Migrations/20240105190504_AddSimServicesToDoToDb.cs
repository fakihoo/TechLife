using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLife.Migrations
{
    /// <inheritdoc />
    public partial class AddSimServicesToDoToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimServiceToDos",
                columns: table => new
                {
                    SimServicesToDoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimServicesToDoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    SimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SimServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimServiceToDos", x => x.SimServicesToDoId);
                    table.ForeignKey(
                        name: "FK_SimServiceToDos_SimServices_SimServiceId",
                        column: x => x.SimServiceId,
                        principalTable: "SimServices",
                        principalColumn: "SimServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimServiceToDos_SimServiceId",
                table: "SimServiceToDos",
                column: "SimServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimServiceToDos");
        }
    }
}
