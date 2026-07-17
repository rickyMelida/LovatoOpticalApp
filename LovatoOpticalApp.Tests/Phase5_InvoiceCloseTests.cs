using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Discounts;
using LovatoOpticalApp.Core.Entities.Payments;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Tests.Fixtures;

namespace LovatoOpticalApp.Tests;

/// <summary>
/// FASE 5 — Cierre y comprobante.
/// Cubre: transición de Invoice a FullyPaid, promoción del pedido a InProduction,
/// GenerateSummary contiene los datos clave, y que el pedido no avanza
/// cuando la factura queda parcialmente pagada.
/// </summary>
public class Phase5_InvoiceCloseTests
{
    // ── Transición de estado de Invoice ──────────────────────────────────────

    [Fact]
    public void Invoice_AfterFullPayment_StateIsFullyPaid()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));

        Assert.Equal(InvoiceStateEnum.FullyPaid, invoice.State);
    }

    [Fact]
    public void Invoice_AfterPartialPayment_StateIsPartiallyPaid()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(1m, 1m));

        Assert.Equal(InvoiceStateEnum.PartiallyPaid, invoice.State);
    }

    [Fact]
    public void Invoice_NoPayments_StateIsPending()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        Assert.Equal(InvoiceStateEnum.Pending, invoice.State);
    }

    [Fact]
    public void Invoice_AfterCancel_StateRemainsAsSet()
    {
        // No hay método Cancel expuesto todavía; verificamos que el estado
        // no cambia si no se registran pagos.
        var invoice = OrderTestFixture.BuildInvoice();

        Assert.NotEqual(InvoiceStateEnum.FullyPaid, invoice.State);
        Assert.NotEqual(InvoiceStateEnum.PartiallyPaid, invoice.State);
    }

    // ── Transición de estado del pedido ─────────────────────────────────────

    [Fact]
    public void Order_AfterFullPayment_StateBecomesInProduction()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));

        Assert.Equal(StateEnum.InProduction, invoice.Order.State);
    }

    [Fact]
    public void Order_AfterPartialPayment_StateRemainesDrafts()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        invoice.RegisterPayment(new CashPayment(1m, 1m));

        Assert.Equal(StateEnum.Drafts, invoice.Order.State);
    }

    [Fact]
    public void Order_AlreadyConfirmed_AfterFullPayment_PromotesToInProduction()
    {
        var order = OrderTestFixture.BuildValidOrder();
        order.State = StateEnum.Confirmed;
        var invoice = new Invoice(order);

        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));

        Assert.Equal(StateEnum.InProduction, invoice.Order.State);
    }

    [Fact]
    public void Order_AlreadyInProduction_StateDoesNotRegress()
    {
        var order = OrderTestFixture.BuildValidOrder();
        order.State = StateEnum.InProduction;
        var invoice = new Invoice(order);

        // Pago que salda la factura; el pedido ya estaba en InProduction
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));

        Assert.Equal(StateEnum.InProduction, invoice.Order.State);
    }

    // ── GenerateSummary ───────────────────────────────────────────────────────

    [Fact]
    public void GenerateSummary_ContainsCustomerName()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        var summary = invoice.GenerateSummary();

        Assert.Contains("Juan Pérez", summary);
    }

    [Fact]
    public void GenerateSummary_ContainsSubTotal()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        var summary = invoice.GenerateSummary();

        Assert.Contains(invoice.SubTotal.ToString("0.00"), summary);
    }

    [Fact]
    public void GenerateSummary_ContainsTotalWithDiscount_WhenDiscountApplied()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.AddDiscount(new DiscountByPercentage(15m));
        var summary = invoice.GenerateSummary();

        Assert.Contains(invoice.TotalWithDiscount.ToString("0.00"), summary);
    }

    [Fact]
    public void GenerateSummary_ContainsDiscountDescription()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.AddDiscount(new DiscountByPercentage(15m, "Obra social OSDE"));
        var summary = invoice.GenerateSummary();

        Assert.Contains("Obra social OSDE", summary);
    }

    [Fact]
    public void GenerateSummary_ContainsPaymentMethodAfterPayment()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));
        var summary = invoice.GenerateSummary();

        Assert.Contains("Cash", summary);
    }

    [Fact]
    public void GenerateSummary_ContainsInvoiceState()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));
        var summary = invoice.GenerateSummary();

        Assert.Contains("FullyPaid", summary);
    }

    [Fact]
    public void GenerateSummary_ContainsBalanceZero_WhenFullyPaid()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));
        var summary = invoice.GenerateSummary();

        // Usar la cultura actual para el separador decimal (puede ser ',' en español)
        Assert.Contains(0m.ToString("0.00"), summary);
    }

    // ── Flujo completo end-to-end (Fases 2→5) ───────────────────────────────

    [Fact]
    public void EndToEnd_BuildOrder_ApplyDiscount_PayCashAndCreditCard_InvoiceFullyPaid()
    {
        // Fase 2: construir pedido
        var order = OrderTestFixture.BuildValidOrder();

        // Fase 3: facturar con descuento 15%
        var invoice = new Invoice(order);
        invoice.AddDiscount(new DiscountByPercentage(15m));
        var totalAfterDiscount = invoice.TotalWithDiscount;
        Assert.True(totalAfterDiscount < invoice.SubTotal);

        // Fase 4a: pago parcial en efectivo
        invoice.RegisterPayment(new CashPayment(150m, 200m));
        Assert.Equal(InvoiceStateEnum.PartiallyPaid, invoice.State);
        Assert.Equal(StateEnum.Drafts, order.State);

        // Fase 4b: resto con tarjeta de crédito 3 cuotas
        var remaining = invoice.Balance;
        invoice.RegisterPayment(new CreditCardPayment(remaining, 3, "Visa"));

        // Fase 5: cierre
        Assert.Equal(0m, invoice.Balance);
        Assert.Equal(InvoiceStateEnum.FullyPaid, invoice.State);
        Assert.Equal(StateEnum.InProduction, order.State);

        var summary = invoice.GenerateSummary();
        Assert.Contains("Juan Pérez", summary);
        Assert.Contains("FullyPaid", summary);
    }

    [Fact]
    public void EndToEnd_BuildOrder_PayWithTransfer_AfterVerification_InvoiceFullyPaid()
    {
        var order = OrderTestFixture.BuildValidOrder();
        var invoice = new Invoice(order);

        // Comprobante sin verificar → no se puede confirmar aún
        var proof = OrderTestFixture.BuildUnverifiedProof();
        Assert.False(proof.IsVerified);

        // Verificar comprobante
        proof.Verify();
        Assert.True(proof.IsVerified);

        // Registrar pago con transferencia confirmada
        invoice.RegisterPayment(new TransferPayment(invoice.Balance, proof));

        Assert.Equal(0m, invoice.Balance);
        Assert.Equal(InvoiceStateEnum.FullyPaid, invoice.State);
        Assert.Equal(StateEnum.InProduction, order.State);
    }
}
