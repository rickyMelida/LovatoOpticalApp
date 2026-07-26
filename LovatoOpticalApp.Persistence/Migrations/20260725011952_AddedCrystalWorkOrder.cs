using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCrystalWorkOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrystalOrderWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrystalRightId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CrystalLeftId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_ESF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_CIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_AXIS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_ADD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_DNP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_HEIGHT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_ESF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_CIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_AXIS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_ADD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_DNP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI_HEIGHT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mounting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horizontal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vertical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorDiagonal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bridge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PantoscopicAngle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanoramicAngle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalOrderWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrystalOrderWorks_Crystals_CrystalLeftId",
                        column: x => x.CrystalLeftId,
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrystalOrderWorks_Crystals_CrystalRightId",
                        column: x => x.CrystalRightId,
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrystalOrderWorks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrystalOrderWorks_CrystalLeftId",
                table: "CrystalOrderWorks",
                column: "CrystalLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalOrderWorks_CrystalRightId",
                table: "CrystalOrderWorks",
                column: "CrystalRightId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalOrderWorks_OrderId",
                table: "CrystalOrderWorks",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrystalOrderWorks");
        }
    }
}
