using Microsoft.EntityFrameworkCore.Migrations;

namespace Jabar.Data.Migrations
{
    public partial class addanotherfieldtoitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAssembled",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssembled",
                table: "Items");
        }
    }
}
