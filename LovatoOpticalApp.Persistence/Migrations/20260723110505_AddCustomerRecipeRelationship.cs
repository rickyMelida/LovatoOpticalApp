using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerRecipeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Recipes_RecipeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RecipeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CustomerId",
                table: "Recipes",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Customers_CustomerId",
                table: "Recipes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Customers_CustomerId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CustomerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Recipes");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RecipeId",
                table: "Customers",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Recipes_RecipeId",
                table: "Customers",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
