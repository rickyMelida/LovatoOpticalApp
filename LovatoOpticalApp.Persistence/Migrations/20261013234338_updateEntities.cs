using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashPayments",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "CreditCardPayments",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "crystal_covering",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "DebitCardPayments",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "DiscountByFixedAmounts",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "DiscountByPercentages",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "TransferPayments",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "Coverings",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "PaymentProofs",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "Crystals",
                schema: "lovato");

            migrationBuilder.DropTable(
                name: "Frames",
                schema: "lovato");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashPayments",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AmountReceived = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPayments", x => x.Id);
                });

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
                name: "CreditCardPayments",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CardBrand = table.Column<string>(type: "text", nullable: false),
                    Installments = table.Column<int>(type: "integer", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crystals",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crystals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebitCardPayments",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    LastFourDigits = table.Column<string>(type: "text", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitCardPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountByFixedAmounts",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FixedAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountByFixedAmounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountByPercentages",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Percentage = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountByPercentages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FrameType = table.Column<int>(type: "integer", nullable: false),
                    Material = table.Column<int>(type: "integer", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProofs",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProofs", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalLeftId = table.Column<Guid>(type: "uuid", nullable: true),
                    CrystalOrderWorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalRightId = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FrameId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observations = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CrystalOrderWorks_CrystalOrderWorkId",
                        column: x => x.CrystalOrderWorkId,
                        principalSchema: "lovato",
                        principalTable: "CrystalOrderWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Crystals_CrystalLeftId",
                        column: x => x.CrystalLeftId,
                        principalSchema: "lovato",
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Crystals_CrystalRightId",
                        column: x => x.CrystalRightId,
                        principalSchema: "lovato",
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "lovato",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Frames_FrameId",
                        column: x => x.FrameId,
                        principalSchema: "lovato",
                        principalTable: "Frames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPayments",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProofId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferPayments_PaymentProofs_ProofId",
                        column: x => x.ProofId,
                        principalSchema: "lovato",
                        principalTable: "PaymentProofs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "lovato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "lovato",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_crystal_covering_CrystalsId",
                schema: "lovato",
                table: "crystal_covering",
                column: "CrystalsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                schema: "lovato",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrystalLeftId",
                schema: "lovato",
                table: "Orders",
                column: "CrystalLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrystalOrderWorkId",
                schema: "lovato",
                table: "Orders",
                column: "CrystalOrderWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrystalRightId",
                schema: "lovato",
                table: "Orders",
                column: "CrystalRightId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "lovato",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FrameId",
                schema: "lovato",
                table: "Orders",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPayments_ProofId",
                schema: "lovato",
                table: "TransferPayments",
                column: "ProofId");
        }
    }
}
