using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class fullnameInCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Companies",
                type: "int",
                nullable: true);
        }
    }
}
