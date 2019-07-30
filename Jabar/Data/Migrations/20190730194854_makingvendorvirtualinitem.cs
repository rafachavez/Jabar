using Microsoft.EntityFrameworkCore.Migrations;

namespace Jabar.Data.Migrations
{
    public partial class makingvendorvirtualinitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Vendors_PreferredVendorVendorId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_PreferredVendorVendorId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PreferredVendorVendorId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_VendorId",
                table: "Items",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Vendors_VendorId",
                table: "Items",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.SetDefault);//LOOK HERE IF THINGS BEHAVE ODDLY ON DELETE
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Vendors_VendorId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_VendorId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "PreferredVendorVendorId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_PreferredVendorVendorId",
                table: "Items",
                column: "PreferredVendorVendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Vendors_PreferredVendorVendorId",
                table: "Items",
                column: "PreferredVendorVendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
