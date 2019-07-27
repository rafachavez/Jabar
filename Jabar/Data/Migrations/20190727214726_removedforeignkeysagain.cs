using Microsoft.EntityFrameworkCore.Migrations;

namespace Jabar.Data.Migrations
{
    public partial class removedforeignkeysagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines");

            migrationBuilder.AlterColumn<int>(
                name: "AssemblyRecipeId",
                table: "RecipeLines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines",
                column: "AssemblyRecipeId",
                principalTable: "AssemblyRecipes",
                principalColumn: "AssemblyRecipeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines");

            migrationBuilder.AlterColumn<int>(
                name: "AssemblyRecipeId",
                table: "RecipeLines",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines",
                column: "AssemblyRecipeId",
                principalTable: "AssemblyRecipes",
                principalColumn: "AssemblyRecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
