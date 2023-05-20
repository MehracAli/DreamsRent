using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class alterCapacityAndDoorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Capacities_CapacityId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Doors_DoorId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Capacities");

            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CapacityId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DoorId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "DoorId",
                table: "Cars",
                newName: "Door");

            migrationBuilder.RenameColumn(
                name: "CapacityId",
                table: "Cars",
                newName: "Capacity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Door",
                table: "Cars",
                newName: "DoorId");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Cars",
                newName: "CapacityId");

            migrationBuilder.CreateTable(
                name: "Capacities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapacityCount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoorCount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CapacityId",
                table: "Cars",
                column: "CapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DoorId",
                table: "Cars",
                column: "DoorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Capacities_CapacityId",
                table: "Cars",
                column: "CapacityId",
                principalTable: "Capacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Doors_DoorId",
                table: "Cars",
                column: "DoorId",
                principalTable: "Doors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
