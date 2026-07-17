using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Discounts;
using LovatoOpticalApp.Core.Entities.Payments;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Tests.Fixtures;

namespace LovatoOpticalApp.Tests;

/// <summary>
/// FASE 3 — Facturación y descuentos.
/// Cubre: SubTotal refleja TotalPrice del pedido, descuento por porcentaje,
/// descuento por monto fijo, descuentos combinados, y restricciones de negocio.
/// </summary>
public class Phase3_InvoiceDiscountTests
{
    // ── SubTotal ─────────────────────────────────────────────────────────────

    [Fact]
    public void Invoice_SubTotal_EqualsOrderTotalPrice()
    {
        var order = OrderTestFixture.BuildValidOrder();
        var invoice = new Invoice(order);

        Assert.Equal(order.TotalPrice, invoice.SubTotal);
    }

    [Fact]
    public void Invoice_WithoutDiscounts_TotalWithDiscountEqualsSubTotal()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        Assert.Equal(invoice.SubTotal, invoice.TotalWithDiscount);
    }

    [Fact]
    public void Invoice_InitialState_IsPending()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        Assert.Equal(InvoiceStateEnum.Pending, invoice.State);
    }

    [Fact]
    public void Invoice_NullOrder_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Invoice(null!));
    }

    // ── DiscountByPercentage ──────────────────────────────────────────────────

    [Fact]
    public void DiscountByPercentage_Calculate_ReturnsCorrectAmount()
    {
        var discount = new DiscountByPercentage(15m);

        var result = discount.Calculate(400m);

        Assert.Equal(60.00m, result);
    }

    [Fact]
    public void DiscountByPercentage_Calculate_RoundsTwoDecimals()
    {
        var discount = new DiscountByPercentage(15m);

        var result = discount.Calculate(401.50m);

        // 401.50 * 0.15 = 60.225 → Math.Round usa banker's rounding → 60.22 (dígito par)
        Assert.Equal(60.22m, result);
    }

    [Fact]
    public void DiscountByPercentage_ZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new DiscountByPercentage(0m));
        Assert.Throws<ArgumentOutOfRangeException>(() => new DiscountByPercentage(-5m));
    }

    [Fact]
    public void DiscountByPercentage_Over100_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new DiscountByPercentage(101m));
    }

    [Fact]
    public void DiscountByPercentage_DefaultDescription_ContainsPercentage()
    {
        var discount = new DiscountByPercentage(20m);

        Assert.Contains("20", discount.Description);
    }

    [Fact]
    public void DiscountByPercentage_CustomDescription_UsesProvidedDescription()
    {
        var discount = new DiscountByPercentage(10m, "Obra social OSDE");

        Assert.Equal("Obra social OSDE", discount.Description);
    }

    // ── DiscountByFixedAmount ────────────────────────────────────────────────

    [Fact]
    public void DiscountByFixedAmount_Calculate_ReturnsFixedAmount()
    {
        var discount = new DiscountByFixedAmount(60.15m);

        var result = discount.Calculate(401.50m);

        Assert.Equal(60.15m, result);
    }

    [Fact]
    public void DiscountByFixedAmount_ExceedsSubTotal_CapsAtSubTotal()
    {
        var discount = new DiscountByFixedAmount(500m);

        var result = discount.Calculate(100m);

        Assert.Equal(100m, result);  // nunca descuenta más que el total
    }

    [Fact]
    public void DiscountByFixedAmount_ZeroOrNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new DiscountByFixedAmount(0m));
        Assert.Throws<ArgumentOutOfRangeException>(() => new DiscountByFixedAmount(-10m));
    }

    // ── Invoice con descuentos aplicados ────────────────────────────────────

    [Fact]
    public void Invoice_AddDiscountByPercentage_ReducesTotalCorrectly()
    {
        var order = OrderTestFixture.BuildValidOrder();       // SubTotal = $452.00
        var invoice = new Invoice(order);
        invoice.AddDiscount(new DiscountByPercentage(15m));   // -$67.80

        Assert.Equal(Math.Round(452m * 0.85m, 2), invoice.TotalWithDiscount);
    }

    [Fact]
    public void Invoice_AddDiscountByFixedAmount_ReducesTotalCorrectly()
    {
        var order = OrderTestFixture.BuildValidOrder();       // SubTotal = $452.00
        var invoice = new Invoice(order);
        invoice.AddDiscount(new DiscountByFixedAmount(60.15m));

        Assert.Equal(452m - 60.15m, invoice.TotalWithDiscount);
    }

    [Fact]
    public void Invoice_AddTwoDiscounts_SumsBothDeductions()
    {
        var order = OrderTestFixture.BuildValidOrder();       // SubTotal = $452.00
        var invoice = new Invoice(order);
        invoice.AddDiscount(new DiscountByPercentage(10m));   // -$45.20
        invoice.AddDiscount(new DiscountByFixedAmount(20m));  // -$20.00

        var expectedDiscount = Math.Round(452m * 0.10m, 2) + 20m;
        Assert.Equal(452m - expectedDiscount, invoice.TotalWithDiscount);
    }

    [Fact]
    public void Invoice_AddDiscount_TotalDiscountNeverExceedsSubTotal()
    {
        var order = OrderTestFixture.BuildValidOrder();
        var invoice = new Invoice(order);
        invoice.AddDiscount(new DiscountByFixedAmount(1000m));

        Assert.Equal(0m, invoice.TotalWithDiscount);
    }

    [Fact]
    public void Invoice_AddDiscount_AfterPayment_ThrowsInvalidOperationException()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));

        Assert.Throws<InvalidOperationException>(() =>
            invoice.AddDiscount(new DiscountByPercentage(10m)));
    }

    [Fact]
    public void Invoice_Discounts_AreReadOnlyFromOutside()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.AddDiscount(new DiscountByPercentage(5m));

        Assert.IsAssignableFrom<IReadOnlyList<LovatoOpticalApp.Core.Interfaces.IDiscount>>(invoice.Discounts);
        Assert.Single(invoice.Discounts);
    }
}
