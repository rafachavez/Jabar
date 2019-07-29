using Microsoft.EntityFrameworkCore.Migrations;

namespace Jabar.Data.Migrations
{
    public partial class tryingsomestuff : Migration
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
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssemblyRecipeId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssemblyId",
                table: "AssemblyRecipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "AssemblyRecipes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeLines_ItemId",
                table: "RecipeLines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyRecipes_AssemblyId",
                table: "AssemblyRecipes",
                column: "AssemblyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyRecipes_ItemId",
                table: "AssemblyRecipes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyHistories_ItemId",
                table: "AssemblyHistories",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssemblyHistories_Items_ItemId",
                table: "AssemblyHistories",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssemblyRecipes_Assemblies_AssemblyId",
                table: "AssemblyRecipes",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "AssemblyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssemblyRecipes_Items_ItemId",
                table: "AssemblyRecipes",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines",
                column: "AssemblyRecipeId",
                principalTable: "AssemblyRecipes",
                principalColumn: "AssemblyRecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLines_Items_ItemId",
                table: "RecipeLines",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssemblyHistories_Items_ItemId",
                table: "AssemblyHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssemblyRecipes_Assemblies_AssemblyId",
                table: "AssemblyRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_AssemblyRecipes_Items_ItemId",
                table: "AssemblyRecipes");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId",
                table: "RecipeLines");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLines_Items_ItemId",
                table: "RecipeLines");

            migrationBuilder.DropIndex(
                name: "IX_RecipeLines_ItemId",
                table: "RecipeLines");

            migrationBuilder.DropIndex(
                name: "IX_AssemblyRecipes_AssemblyId",
                table: "AssemblyRecipes");

            migrationBuilder.DropIndex(
                name: "IX_AssemblyRecipes_ItemId",
                table: "AssemblyRecipes");

            migrationBuilder.DropIndex(
                name: "IX_AssemblyHistories_ItemId",
                table: "AssemblyHistories");

            migrationBuilder.DropColumn(
                name: "AssemblyRecipeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AssemblyId",
                table: "AssemblyRecipes");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "AssemblyRecipes");

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
    }
}
