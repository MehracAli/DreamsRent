using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class removeDrivertrianIdfromCarSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivetrians_DrivetrianId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DrivetrianId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DrivetrianId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrivetrianId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DrivetrianId",
                table: "Cars",
                column: "DrivetrianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivetrians_DrivetrianId",
                table: "Cars",
                column: "DrivetrianId",
                principalTable: "Drivetrians",
                principalColumn: "Id");
        }
    }
}
