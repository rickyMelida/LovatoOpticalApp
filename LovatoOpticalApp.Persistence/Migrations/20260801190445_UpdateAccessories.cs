using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccessories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlassesCases",
                schema: "lovato");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "lovato",
                table: "Accessories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "lovato",
                table: "Accessories",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "lovato",
                table: "Accessories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "lovato",
                table: "Accessories");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "lovato",
                table: "Accessories");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "lovato",
                table: "Accessories");

            migrationBuilder.CreateTable(
                name: "GlassesCases",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsOptional = table.Column<bool>(type: "boolean", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassesCases", x => x.Id);
                });
        }
    }
}
