using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LovatoOpticalApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirtsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsOptional = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashPayments",
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
                name: "CreditCardPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Installments = table.Column<int>(type: "integer", nullable: false),
                    CardBrand = table.Column<string>(type: "text", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crystals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TechnicalCharacteristics = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false),
                    Prescription_Sphere = table.Column<decimal>(type: "numeric", nullable: true),
                    Prescription_Cylinder = table.Column<decimal>(type: "numeric", nullable: true),
                    Prescription_Axis = table.Column<int>(type: "integer", nullable: true),
                    Prescription_Addition = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crystals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CiRuc = table.Column<string>(type: "text", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebitCardPayments",
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
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Material = table.Column<int>(type: "integer", nullable: false),
                    FrameType = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlassesCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsOptional = table.Column<bool>(type: "boolean", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassesCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProofs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProofs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrystalTreatment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrystalTreatment", x => new { x.CrystalId, x.Id });
                    table.ForeignKey(
                        name: "FK_CrystalTreatment_Crystals_CrystalId",
                        column: x => x.CrystalId,
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrescriptionIssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Optometrist = table.Column<string>(type: "text", nullable: false),
                    VL_OD_ESF = table.Column<string>(type: "text", nullable: false),
                    VL_OD_CIL = table.Column<string>(type: "text", nullable: false),
                    VL_OD_EJE = table.Column<string>(type: "text", nullable: false),
                    VL_OI_ESF = table.Column<string>(type: "text", nullable: false),
                    VL_OI_CIL = table.Column<string>(type: "text", nullable: false),
                    VL_OI_EJE = table.Column<string>(type: "text", nullable: false),
                    VC_OD_ESF = table.Column<string>(type: "text", nullable: false),
                    VC_OD_CIL = table.Column<string>(type: "text", nullable: false),
                    VC_OD_EJE = table.Column<string>(type: "text", nullable: false),
                    VC_OI_ESF = table.Column<string>(type: "text", nullable: false),
                    VC_OI_CIL = table.Column<string>(type: "text", nullable: false),
                    VC_OI_EJE = table.Column<string>(type: "text", nullable: false),
                    Adicion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FrameId = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalLeftId = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalRightId = table.Column<Guid>(type: "uuid", nullable: false),
                    Observations = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Crystals_CrystalLeftId",
                        column: x => x.CrystalLeftId,
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Crystals_CrystalRightId",
                        column: x => x.CrystalRightId,
                        principalTable: "Crystals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Frames_FrameId",
                        column: x => x.FrameId,
                        principalTable: "Frames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ProofId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferPayments_PaymentProofs_ProofId",
                        column: x => x.ProofId,
                        principalTable: "PaymentProofs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrystalOrderWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CrystalRightId = table.Column<Guid>(type: "uuid", nullable: true),
                    CrystalLeftId = table.Column<Guid>(type: "uuid", nullable: true),
                    Material = table.Column<string>(type: "text", nullable: false),
                    Index = table.Column<string>(type: "text", nullable: false),
                    TreatmentNotes = table.Column<string>(type: "text", nullable: false),
                    OD_ESF = table.Column<string>(type: "text", nullable: false),
                    OD_CIL = table.Column<string>(type: "text", nullable: false),
                    OD_AXIS = table.Column<string>(type: "text", nullable: false),
                    OD_ADD = table.Column<string>(type: "text", nullable: false),
                    OD_DNP = table.Column<string>(type: "text", nullable: false),
                    OD_HEIGHT = table.Column<string>(type: "text", nullable: false),
                    OI_ESF = table.Column<string>(type: "text", nullable: false),
                    OI_CIL = table.Column<string>(type: "text", nullable: false),
                    OI_AXIS = table.Column<string>(type: "text", nullable: false),
                    OI_ADD = table.Column<string>(type: "text", nullable: false),
                    OI_DNP = table.Column<string>(type: "text", nullable: false),
                    OI_HEIGHT = table.Column<string>(type: "text", nullable: false),
                    Mounting = table.Column<string>(type: "text", nullable: false),
                    Horizontal = table.Column<string>(type: "text", nullable: false),
                    Vertical = table.Column<string>(type: "text", nullable: false),
                    MajorDiagonal = table.Column<string>(type: "text", nullable: false),
                    Bridge = table.Column<string>(type: "text", nullable: false),
                    PantoscopicAngle = table.Column<string>(type: "text", nullable: false),
                    PanoramicAngle = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrystalLeftId",
                table: "Orders",
                column: "CrystalLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrystalRightId",
                table: "Orders",
                column: "CrystalRightId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FrameId",
                table: "Orders",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CustomerId",
                table: "Recipes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPayments_ProofId",
                table: "TransferPayments",
                column: "ProofId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "CashPayments");

            migrationBuilder.DropTable(
                name: "CreditCardPayments");

            migrationBuilder.DropTable(
                name: "CrystalOrderWorks");

            migrationBuilder.DropTable(
                name: "CrystalTreatment");

            migrationBuilder.DropTable(
                name: "DebitCardPayments");

            migrationBuilder.DropTable(
                name: "DiscountByFixedAmounts");

            migrationBuilder.DropTable(
                name: "DiscountByPercentages");

            migrationBuilder.DropTable(
                name: "GlassesCases");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "TransferPayments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentProofs");

            migrationBuilder.DropTable(
                name: "Crystals");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Frames");
        }
    }
}
