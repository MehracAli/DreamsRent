using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class removeDrivertrianIdfromCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivetrians_DrivetrianId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DrivertrianId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "DrivetrianId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivetrians_DrivetrianId",
                table: "Cars",
                column: "DrivetrianId",
                principalTable: "Drivetrians",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivetrians_DrivetrianId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "DrivetrianId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrivertrianId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivetrians_DrivetrianId",
                table: "Cars",
                column: "DrivetrianId",
                principalTable: "Drivetrians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
