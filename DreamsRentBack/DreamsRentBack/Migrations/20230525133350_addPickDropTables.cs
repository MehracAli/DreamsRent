using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamsRentBack.Migrations
{
    public partial class addPickDropTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDropoffLocation_Companies_CompanyId",
                table: "CompanyDropoffLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDropoffLocation_DropoffLocations_DropoffLocationId",
                table: "CompanyDropoffLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPickupLocation_Companies_CompanyId",
                table: "CompanyPickupLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPickupLocation_PickupLocations_PickupLocationId",
                table: "CompanyPickupLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPickupLocation",
                table: "CompanyPickupLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyDropoffLocation",
                table: "CompanyDropoffLocation");

            migrationBuilder.RenameTable(
                name: "CompanyPickupLocation",
                newName: "CompanyPickupLocations");

            migrationBuilder.RenameTable(
                name: "CompanyDropoffLocation",
                newName: "CompanyDropoffLocations");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPickupLocation_PickupLocationId",
                table: "CompanyPickupLocations",
                newName: "IX_CompanyPickupLocations_PickupLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPickupLocation_CompanyId",
                table: "CompanyPickupLocations",
                newName: "IX_CompanyPickupLocations_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyDropoffLocation_DropoffLocationId",
                table: "CompanyDropoffLocations",
                newName: "IX_CompanyDropoffLocations_DropoffLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyDropoffLocation_CompanyId",
                table: "CompanyDropoffLocations",
                newName: "IX_CompanyDropoffLocations_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPickupLocations",
                table: "CompanyPickupLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyDropoffLocations",
                table: "CompanyDropoffLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDropoffLocations_Companies_CompanyId",
                table: "CompanyDropoffLocations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDropoffLocations_DropoffLocations_DropoffLocationId",
                table: "CompanyDropoffLocations",
                column: "DropoffLocationId",
                principalTable: "DropoffLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPickupLocations_Companies_CompanyId",
                table: "CompanyPickupLocations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPickupLocations_PickupLocations_PickupLocationId",
                table: "CompanyPickupLocations",
                column: "PickupLocationId",
                principalTable: "PickupLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDropoffLocations_Companies_CompanyId",
                table: "CompanyDropoffLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDropoffLocations_DropoffLocations_DropoffLocationId",
                table: "CompanyDropoffLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPickupLocations_Companies_CompanyId",
                table: "CompanyPickupLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPickupLocations_PickupLocations_PickupLocationId",
                table: "CompanyPickupLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPickupLocations",
                table: "CompanyPickupLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyDropoffLocations",
                table: "CompanyDropoffLocations");

            migrationBuilder.RenameTable(
                name: "CompanyPickupLocations",
                newName: "CompanyPickupLocation");

            migrationBuilder.RenameTable(
                name: "CompanyDropoffLocations",
                newName: "CompanyDropoffLocation");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPickupLocations_PickupLocationId",
                table: "CompanyPickupLocation",
                newName: "IX_CompanyPickupLocation_PickupLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPickupLocations_CompanyId",
                table: "CompanyPickupLocation",
                newName: "IX_CompanyPickupLocation_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyDropoffLocations_DropoffLocationId",
                table: "CompanyDropoffLocation",
                newName: "IX_CompanyDropoffLocation_DropoffLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyDropoffLocations_CompanyId",
                table: "CompanyDropoffLocation",
                newName: "IX_CompanyDropoffLocation_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPickupLocation",
                table: "CompanyPickupLocation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyDropoffLocation",
                table: "CompanyDropoffLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDropoffLocation_Companies_CompanyId",
                table: "CompanyDropoffLocation",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDropoffLocation_DropoffLocations_DropoffLocationId",
                table: "CompanyDropoffLocation",
                column: "DropoffLocationId",
                principalTable: "DropoffLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPickupLocation_Companies_CompanyId",
                table: "CompanyPickupLocation",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPickupLocation_PickupLocations_PickupLocationId",
                table: "CompanyPickupLocation",
                column: "PickupLocationId",
                principalTable: "PickupLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
