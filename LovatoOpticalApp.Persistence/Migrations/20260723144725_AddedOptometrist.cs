using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptometrist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Optometrist",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Optometrist",
                table: "Recipes");
        }
    }
}
