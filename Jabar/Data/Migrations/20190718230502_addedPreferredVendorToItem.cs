using Microsoft.EntityFrameworkCore.Migrations;

namespace Jabar.Data.Migrations
{
    public partial class addedPreferredVendorToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
