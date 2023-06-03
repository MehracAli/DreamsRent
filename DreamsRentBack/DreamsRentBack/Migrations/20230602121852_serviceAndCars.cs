using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class serviceAndCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraServicesAndCars_Cars_CarId",
                table: "ExtraServicesAndCars");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtraServicesAndCars_ExtraServices_ExtraServiceId",
                table: "ExtraServicesAndCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtraServicesAndCars",
                table: "ExtraServicesAndCars");

            migrationBuilder.RenameTable(
                name: "ExtraServicesAndCars",
                newName: "ServicesAndCars");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraServicesAndCars_ExtraServiceId",
                table: "ServicesAndCars",
                newName: "IX_ServicesAndCars_ExtraServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtraServicesAndCars_CarId",
                table: "ServicesAndCars",
                newName: "IX_ServicesAndCars_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicesAndCars",
                table: "ServicesAndCars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesAndCars_Cars_CarId",
                table: "ServicesAndCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesAndCars_ExtraServices_ExtraServiceId",
                table: "ServicesAndCars",
                column: "ExtraServiceId",
                principalTable: "ExtraServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesAndCars_Cars_CarId",
                table: "ServicesAndCars");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicesAndCars_ExtraServices_ExtraServiceId",
                table: "ServicesAndCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicesAndCars",
                table: "ServicesAndCars");

            migrationBuilder.RenameTable(
                name: "ServicesAndCars",
                newName: "ExtraServicesAndCars");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesAndCars_ExtraServiceId",
                table: "ExtraServicesAndCars",
                newName: "IX_ExtraServicesAndCars_ExtraServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicesAndCars_CarId",
                table: "ExtraServicesAndCars",
                newName: "IX_ExtraServicesAndCars_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtraServicesAndCars",
                table: "ExtraServicesAndCars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraServicesAndCars_Cars_CarId",
                table: "ExtraServicesAndCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraServicesAndCars_ExtraServices_ExtraServiceId",
                table: "ExtraServicesAndCars",
                column: "ExtraServiceId",
                principalTable: "ExtraServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
