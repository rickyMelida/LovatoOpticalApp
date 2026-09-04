using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrystalOrderWorks_Crystals_CrystalLeftId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_CrystalOrderWorks_Crystals_CrystalRightId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_CrystalOrderWorks_Orders_OrderId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropTable(
                name: "CrystalTreatment",
                schema: "lovato");

            migrationBuilder.DropIndex(
                name: "IX_CrystalOrderWorks_CrystalLeftId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropIndex(
                name: "IX_CrystalOrderWorks_CrystalRightId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropIndex(
                name: "IX_CrystalOrderWorks_OrderId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropColumn(
                name: "Prescription_Addition",
                schema: "lovato",
                table: "Crystals");

            migrationBuilder.DropColumn(
                name: "Prescription_Axis",
                schema: "lovato",
                table: "Crystals");

            migrationBuilder.DropColumn(
                name: "Prescription_Cylinder",
                schema: "lovato",
                table: "Crystals");

            migrationBuilder.DropColumn(
                name: "Prescription_Sphere",
                schema: "lovato",
                table: "Crystals");

            migrationBuilder.DropColumn(
                name: "CrystalLeftId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropColumn(
                name: "CrystalRightId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "lovato",
                table: "Accessories");

            migrationBuilder.RenameColumn(
                name: "TechnicalCharacteristics",
                schema: "lovato",
                table: "Crystals",
                newName: "Description");

            migrationBuilder.AlterColumn<Guid>(
                name: "FrameId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CrystalRightId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CrystalLeftId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "lovato",
                table: "Crystals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                ALTER TABLE lovato.""CrystalOrderWorks""
                ALTER COLUMN ""Index"" TYPE integer
                USING COALESCE(NULLIF(""Index"", ''), '0')::integer;
            ");

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                schema: "lovato",
                table: "CrystalOrderWorks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders",
                column: "CrystalOrderWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CrystalOrderWorks_CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders",
                column: "CrystalOrderWorkId",
                principalSchema: "lovato",
                principalTable: "CrystalOrderWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CrystalOrderWorks_CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "lovato",
                table: "Crystals");

            migrationBuilder.DropColumn(
                name: "Observation",
                schema: "lovato",
                table: "CrystalOrderWorks");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "lovato",
                table: "Crystals",
                newName: "TechnicalCharacteristics");

            migrationBuilder.AlterColumn<Guid>(
                name: "FrameId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CrystalRightId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CrystalLeftId",
                schema: "lovato",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prescription_Addition",
                schema: "lovato",
                table: "Crystals",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Prescription_Axis",
                schema: "lovato",
                table: "Crystals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prescription_Cylinder",
                schema: "lovato",
                table: "Crystals",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Prescription_Sphere",
                schema: "lovato",
                table: "Crystals",
                type: "numeric",
                nullable: true);

            migrationBuilder.Sql(@"
                ALTER TABLE lovato.""CrystalOrderWorks""
                ALTER COLUMN ""Index"" TYPE text
                USING ""Index""::text;
            ");

            migrationBuilder.AddColumn<Guid>(
                name: "CrystalLeftId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CrystalRightId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "lovato",
                table: "Accessories",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CrystalTreatment",
                schema: "lovato",
                columns: table => new
                {
                    CrystalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTreatment", x => new { x.CrystalId, x.Id });
                    table.ForeignKey(
                        name: "FK_CrystalTreatment_Crystals_CrystalId",
                        column: x => x.CrystalId,
                        principalSchema: "lovato",
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrystalOrderWorks_CrystalLeftId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                column: "CrystalLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalOrderWorks_CrystalRightId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                column: "CrystalRightId");

            migrationBuilder.CreateIndex(
                name: "IX_CrystalOrderWorks_OrderId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalOrderWorks_Crystals_CrystalLeftId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                column: "CrystalLeftId",
                principalSchema: "lovato",
                principalTable: "Crystals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalOrderWorks_Crystals_CrystalRightId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                column: "CrystalRightId",
                principalSchema: "lovato",
                principalTable: "Crystals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrystalOrderWorks_Orders_OrderId",
                schema: "lovato",
                table: "CrystalOrderWorks",
                column: "OrderId",
                principalSchema: "lovato",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
