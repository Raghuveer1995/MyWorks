using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateListing.Migrations
{
    public partial class SeedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Asset",
                columns: new[] { "Id", "Location", "SoldDate", "SoldTo", "Type" },
                values: new object[,]
                {
                    { 1, "Cincinnati", null, null, "Open Land" },
                    { 2, "Columbus", null, null, "Apartment" },
                    { 3, "Cincinnati", null, null, "Farm House" },
                    { 4, "Dayton", null, null, "Building" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Asset",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
