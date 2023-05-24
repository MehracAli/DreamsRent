using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class PickuoDropoffTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DropoffLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropoffLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DropoffLocations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PickupLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupLocations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDropoffLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DropoffLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDropoffLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDropoffLocation_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDropoffLocation_DropoffLocations_DropoffLocationId",
                        column: x => x.DropoffLocationId,
                        principalTable: "DropoffLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPickupLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PickupLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPickupLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPickupLocation_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPickupLocation_PickupLocations_PickupLocationId",
                        column: x => x.PickupLocationId,
                        principalTable: "PickupLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDropoffLocation_CompanyId",
                table: "CompanyDropoffLocation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDropoffLocation_DropoffLocationId",
                table: "CompanyDropoffLocation",
                column: "DropoffLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPickupLocation_CompanyId",
                table: "CompanyPickupLocation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPickupLocation_PickupLocationId",
                table: "CompanyPickupLocation",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DropoffLocations_CityId",
                table: "DropoffLocations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocations_CityId",
                table: "PickupLocations",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDropoffLocation");

            migrationBuilder.DropTable(
                name: "CompanyPickupLocation");

            migrationBuilder.DropTable(
                name: "DropoffLocations");

            migrationBuilder.DropTable(
                name: "PickupLocations");
        }
    }
}
