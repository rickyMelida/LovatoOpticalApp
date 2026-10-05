using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CrearCristalesYRecubrimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coverings",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "crystal_covering",
                schema: "lovato",
                columns: table => new
                {
                    CoveringsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crystal_covering", x => new { x.CoveringsId, x.CrystalsId });
                    table.ForeignKey(
                        name: "FK_crystal_covering_Coverings_CoveringsId",
                        column: x => x.CoveringsId,
                        principalSchema: "lovato",
                        principalTable: "Coverings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_crystal_covering_Crystals_CrystalsId",
                        column: x => x.CrystalsId,
                        principalSchema: "lovato",
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_crystal_covering_CrystalsId",
                schema: "lovato",
                table: "crystal_covering",
                column: "CrystalsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crystal_covering",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "Coverings",
                schema: "lovato");
        }
    }
}
