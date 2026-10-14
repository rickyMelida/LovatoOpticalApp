using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedPricesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoatingTypes",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoatingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrystalAdditionConfigurations",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerDiopter = table.Column<decimal>(type: "numeric", nullable: false),
                    EffectivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalAdditionConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrystalBrands",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrystalTypes",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoatingPrices",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CoatingTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    EffectivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoatingPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoatingPrices_CoatingTypes_CoatingTypeId",
                        column: x => x.CoatingTypeId,
                        principalSchema: "lovato",
                        principalTable: "CoatingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalBrandPrices",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalBrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    EffectivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalBrandPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalBrandPrices_CrystalBrands_CrystalBrandId",
                        column: x => x.CrystalBrandId,
                        principalSchema: "lovato",
                        principalTable: "CrystalBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalPrices",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    EffectivityDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalPrices_CrystalTypes_CrystalTypeId",
                        column: x => x.CrystalTypeId,
                        principalSchema: "lovato",
                        principalTable: "CrystalTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoatingPrices_CoatingTypeId",
                schema: "lovato",
                table: "CoatingPrices",
                column: "CoatingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalBrandPrices_CrystalBrandId",
                schema: "lovato",
                table: "CrystalBrandPrices",
                column: "CrystalBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalPrices_CrystalTypeId",
                schema: "lovato",
                table: "CrystalPrices",
                column: "CrystalTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoatingPrices",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CrystalAdditionConfigurations",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CrystalBrandPrices",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CrystalPrices",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CoatingTypes",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CrystalBrands",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CrystalTypes",
                schema: "lovato");
        }
    }
}
