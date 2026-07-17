using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Discounts;
using LovatoOpticalApp.Core.Entities.Payments;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Tests.Fixtures;

namespace LovatoOpticalApp.Tests;

/// <summary>
/// FASE 4 — Registro de pagos.
/// Cubre: CashPayment (vuelto), CreditCardPayment (cuotas),
/// DebitCardPayment, TransferPayment (comprobante), pago parcial,
/// pago que supera el saldo, y múltiples pagos combinados.
/// </summary>
public class Phase4_PaymentTests
{
    // ── CashPayment ──────────────────────────────────────────────────────────

    [Fact]
    public void CashPayment_Change_CalculatesCorrectly()
    {
        var payment = new CashPayment(amount: 150m, amountReceived: 200m);

        Assert.Equal(50m, payment.Change);
    }

    [Fact]
    public void CashPayment_ExactAmount_ChangeIsZero()
    {
        var payment = new CashPayment(amount: 150m, amountReceived: 150m);

        Assert.Equal(0m, payment.Change);
    }

    [Fact]
    public void CashPayment_AmountReceivedLessThanAmount_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new CashPayment(amount: 200m, amountReceived: 100m));
    }

    [Fact]
    public void CashPayment_Method_IsCash()
    {
        var payment = new CashPayment(100m, 100m);
        Assert.Equal(PaymentMethodEnum.Cash, payment.Method);
    }

    // ── CreditCardPayment ────────────────────────────────────────────────────

    [Fact]
    public void CreditCardPayment_InstallmentValue_CalculatesCorrectly()
    {
        // $191.35 / 3 cuotas = $63.78 (redondeado)
        var payment = new CreditCardPayment(191.35m, 3, "Visa");

        Assert.Equal(63.78m, payment.InstallmentValue);
    }

    [Fact]
    public void CreditCardPayment_SingleInstallment_InstallmentValueEqualsAmount()
    {
        var payment = new CreditCardPayment(100m, 1);

        Assert.Equal(100m, payment.InstallmentValue);
    }

    [Fact]
    public void CreditCardPayment_ZeroInstallments_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CreditCardPayment(100m, 0));
    }

    [Fact]
    public void CreditCardPayment_Method_IsCreditCard()
    {
        var payment = new CreditCardPayment(100m, 1);
        Assert.Equal(PaymentMethodEnum.CreditCard, payment.Method);
    }

    // ── DebitCardPayment ─────────────────────────────────────────────────────

    [Fact]
    public void DebitCardPayment_StoresBankAndLastDigits()
    {
        var payment = new DebitCardPayment(191.35m, "Banco Galicia", "4321");

        Assert.Equal("Banco Galicia", payment.Bank);
        Assert.Equal("4321", payment.LastFourDigits);
    }

    [Fact]
    public void DebitCardPayment_Method_IsDebitCard()
    {
        var payment = new DebitCardPayment(100m, "Banco", "0000");
        Assert.Equal(PaymentMethodEnum.DebitCard, payment.Method);
    }

    // ── TransferPayment + PaymentProof ───────────────────────────────────────

    [Fact]
    public void TransferPayment_InitialState_IsNotConfirmed()
    {
        var proof = OrderTestFixture.BuildUnverifiedProof();
        var payment = new TransferPayment(191.35m, proof);

        Assert.False(payment.IsConfirmed);
    }

    [Fact]
    public void TransferPayment_AfterProofVerified_IsConfirmed()
    {
        var proof = OrderTestFixture.BuildUnverifiedProof();
        var payment = new TransferPayment(191.35m, proof);

        proof.Verify();

        Assert.True(payment.IsConfirmed);
    }

    [Fact]
    public void TransferPayment_NullProof_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TransferPayment(100m, null!));
    }

    [Fact]
    public void TransferPayment_Method_IsBankTransfer()
    {
        var payment = new TransferPayment(100m, OrderTestFixture.BuildUnverifiedProof());
        Assert.Equal(PaymentMethodEnum.BankTransfer, payment.Method);
    }

    [Fact]
    public void PaymentProof_BeforeVerify_IsNotVerified()
    {
        var proof = OrderTestFixture.BuildUnverifiedProof();
        Assert.False(proof.IsVerified);
    }

    [Fact]
    public void PaymentProof_AfterVerify_IsVerified()
    {
        var proof = OrderTestFixture.BuildUnverifiedProof();
        proof.Verify();
        Assert.True(proof.IsVerified);
    }

    // ── Invoice.RegisterPayment — flujo del diagrama ─────────────────────────

    [Fact]
    public void RegisterPayment_Cash_ReducesBalanceCorrectly()
    {
        // Diagrama: pagar $150 en efectivo (de $452 total) → saldo $302
        var invoice = OrderTestFixture.BuildInvoice();   // SubTotal = $452
        var cash = new CashPayment(150m, 200m);

        invoice.RegisterPayment(cash);

        Assert.Equal(452m - 150m, invoice.Balance);
    }

    [Fact]
    public void RegisterPayment_Cash_StateBecomesPartiallyPaid()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(100m, 100m));

        Assert.Equal(InvoiceStateEnum.PartiallyPaid, invoice.State);
    }

    [Fact]
    public void RegisterPayment_FullAmount_StateBecomesFullyPaid()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(invoice.Balance, invoice.Balance));

        Assert.Equal(InvoiceStateEnum.FullyPaid, invoice.State);
        Assert.Equal(0m, invoice.Balance);
    }

    [Fact]
    public void RegisterPayment_ExceedsBalance_ThrowsInvalidOperationException()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        Assert.Throws<InvalidOperationException>(() =>
            invoice.RegisterPayment(new CashPayment(invoice.Balance + 1m, invoice.Balance + 1m)));
    }

    [Fact]
    public void RegisterPayment_ZeroAmount_ThrowsArgumentException()
    {
        var invoice = OrderTestFixture.BuildInvoice();

        // CashPayment with 0 amount is blocked by CashPayment itself;
        // we test Invoice directly with a custom stub-like scenario via DebitCard
        // (DebitCardPayment allows 0 by default, so Invoice's guard catches it)
        Assert.Throws<ArgumentException>(() =>
            invoice.RegisterPayment(new DebitCardPayment(0m, "Banco", "1234")));
    }

    [Fact]
    public void RegisterPayment_TwoPartialPayments_BalanceIsCorrect()
    {
        // Diagrama: $150 efectivo + $302 tarjeta débito = saldo $0
        var invoice = OrderTestFixture.BuildInvoice();   // $452
        invoice.RegisterPayment(new CashPayment(150m, 150m));
        invoice.RegisterPayment(new DebitCardPayment(302m, "Banco Nación", "9876"));

        Assert.Equal(0m, invoice.Balance);
        Assert.Equal(InvoiceStateEnum.FullyPaid, invoice.State);
    }

    [Fact]
    public void RegisterPayment_CashThenCreditCard_BalanceIsZero()
    {
        // Reproduce el flujo exacto del diagrama con descuento
        // Invoice con descuento 15% sobre $452 = $383.00 (aprox)
        var order = OrderTestFixture.BuildValidOrder();
        var invoice = new Invoice(order);
        invoice.AddDiscount(new DiscountByPercentage(15m));

        var totalAfterDiscount = invoice.TotalWithDiscount;

        // Pago parcial en efectivo ($150)
        invoice.RegisterPayment(new CashPayment(150m, 200m));
        var remaining = totalAfterDiscount - 150m;

        // Pago del resto con tarjeta de crédito en 3 cuotas
        var creditCard = new CreditCardPayment(remaining, 3, "Visa");
        invoice.RegisterPayment(creditCard);

        Assert.Equal(0m, invoice.Balance);
        Assert.Equal(Math.Round(remaining / 3, 2), creditCard.InstallmentValue);
    }

    [Fact]
    public void RegisterPayment_TransferAfterVerification_BalanceIsZero()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        var proof = OrderTestFixture.BuildUnverifiedProof();
        proof.Verify();

        var transfer = new TransferPayment(invoice.Balance, proof);
        invoice.RegisterPayment(transfer);

        Assert.Equal(0m, invoice.Balance);
        Assert.True(transfer.IsConfirmed);
    }

    [Fact]
    public void Payments_AreReadOnlyFromOutside()
    {
        var invoice = OrderTestFixture.BuildInvoice();
        invoice.RegisterPayment(new CashPayment(100m, 100m));

        Assert.IsAssignableFrom<IReadOnlyList<LovatoOpticalApp.Core.Interfaces.IPayment>>(invoice.Payments);
        Assert.Single(invoice.Payments);
    }
}
