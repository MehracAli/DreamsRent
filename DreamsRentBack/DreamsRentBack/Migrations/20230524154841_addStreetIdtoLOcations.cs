using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class addStreetIdtoLOcations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "PickupLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                table: "DropoffLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "PickupLocations");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "DropoffLocations");
        }
    }
}
