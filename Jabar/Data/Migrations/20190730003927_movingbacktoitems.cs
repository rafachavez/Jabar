using Microsoft.EntityFrameworkCore.Migrations;

namespace Jabar.Data.Migrations
{
    public partial class movingbacktoitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines");

            migrationBuilder.DropIndex(
                name: "IX_AssemblyRecipes_ItemId",
                table: "AssemblyRecipes");

            migrationBuilder.AlterColumn<int>(
                name: "AssemblyRecipeId",
                table: "RecipeLines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyRecipes_ItemId",
                table: "AssemblyRecipes",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines",
                column: "AssemblyRecipeId",
                principalTable: "AssemblyRecipes",
                principalColumn: "AssemblyRecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines");

            migrationBuilder.DropIndex(
                name: "IX_AssemblyRecipes_ItemId",
                table: "AssemblyRecipes");

            migrationBuilder.AlterColumn<int>(
                name: "AssemblyRecipeId",
                table: "RecipeLines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyRecipes_ItemId",
                table: "AssemblyRecipes",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines",
                column: "AssemblyRecipeId",
                principalTable: "AssemblyRecipes",
                principalColumn: "AssemblyRecipeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
