using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.ValueObjects;
using LovatoOpticalApp.Tests.Fixtures;

namespace LovatoOpticalApp.Tests;

/// <summary>
/// FASE 2 — Construcción del pedido (OrderBuilder).
/// Cubre: fluent API, validaciones de campos obligatorios, cálculo de TotalPrice,
/// cristales con receta y tratamientos, y manejo de accesorios opcionales.
/// </summary>
public class Phase2_OrderBuilderTests
{
    // ── Build exitoso ────────────────────────────────────────────────────────

    [Fact]
    public void Build_WithAllRequiredFields_ReturnsOrder()
    {
        var order = OrderTestFixture.BuildValidOrder();

        Assert.NotNull(order);
        Assert.Equal(StateEnum.Drafts, order.State);
    }

    [Fact]
    public void Build_AssignsCustomerCorrectly()
    {
        var order = OrderTestFixture.BuildValidOrder();

        Assert.NotNull(order.Customer);
        Assert.Equal("Juan Pérez", order.Customer.Name);
    }

    [Fact]
    public void Build_AssignsFrameCorrectly()
    {
        var order = OrderTestFixture.BuildValidOrder();

        Assert.NotNull(order.Frame);
        Assert.Equal("Ray-Ban RB5154", order.Frame.Name);
    }

    [Fact]
    public void Build_AssignsDistinctCrystalsPerEye()
    {
        var right = OrderTestFixture.DefaultCrystal(85.00m, OrderTestFixture.RightEyePrescription());
        var left = OrderTestFixture.DefaultCrystal(85.00m, OrderTestFixture.LeftEyePrescription());

        var order = new OrderBuilder()
            .ForCustomer(OrderTestFixture.DefaultCustomer())
            .WithFrame(OrderTestFixture.DefaultFrame())
            .WithRightCrystal(right)
            .WithLeftCrystal(left)
            .WithGlassesCase(OrderTestFixture.DefaultGlassesCase())
            .Build();

        Assert.NotSame(order.CrystalRight, order.CrystalLeft);
        Assert.Equal(-2.50m, order.CrystalRight!.Prescription!.Sphere);
        Assert.Equal(-1.75m, order.CrystalLeft!.Prescription!.Sphere);
    }

    [Fact]
    public void Build_WithSameCrystals_AssignsBothEyes()
    {
        var crystal = OrderTestFixture.DefaultCrystal();

        var order = new OrderBuilder()
            .ForCustomer(OrderTestFixture.DefaultCustomer())
            .WithFrame(OrderTestFixture.DefaultFrame())
            .WithSameCrystals(crystal)
            .WithGlassesCase(OrderTestFixture.DefaultGlassesCase())
            .Build();

        Assert.Same(order.CrystalRight, order.CrystalLeft);
    }

    [Fact]
    public void Build_AssignsGlassesCaseAsDedicatedField()
    {
        var order = OrderTestFixture.BuildValidOrder();

        Assert.NotNull(order.GlassesCase);
        Assert.Equal("Estuche Rígido Premium", order.GlassesCase.Name);
    }

    [Fact]
    public void Build_AddsOptionalAccessories()
    {
        var order = OrderTestFixture.BuildValidOrder();

        Assert.Equal(2, order.Accessories.Count);
    }

    // ── Validaciones — campos obligatorios ──────────────────────────────────

    [Fact]
    public void Build_WithoutCustomer_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            new OrderBuilder()
                .WithFrame(OrderTestFixture.DefaultFrame())
                .WithSameCrystals(OrderTestFixture.DefaultCrystal())
                .WithGlassesCase(OrderTestFixture.DefaultGlassesCase())
                .Build());

        Assert.Contains("cliente", ex.Message);
    }

    [Fact]
    public void Build_WithoutFrame_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            new OrderBuilder()
                .ForCustomer(OrderTestFixture.DefaultCustomer())
                .WithSameCrystals(OrderTestFixture.DefaultCrystal())
                .WithGlassesCase(OrderTestFixture.DefaultGlassesCase())
                .Build());

        Assert.Contains("armazón", ex.Message);
    }

    [Fact]
    public void Build_WithoutCrystals_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            new OrderBuilder()
                .ForCustomer(OrderTestFixture.DefaultCustomer())
                .WithFrame(OrderTestFixture.DefaultFrame())
                .WithGlassesCase(OrderTestFixture.DefaultGlassesCase())
                .Build());

        Assert.Contains("cristal", ex.Message);
    }

    [Fact]
    public void Build_WithoutGlassesCase_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            new OrderBuilder()
                .ForCustomer(OrderTestFixture.DefaultCustomer())
                .WithFrame(OrderTestFixture.DefaultFrame())
                .WithSameCrystals(OrderTestFixture.DefaultCrystal())
                .Build());

        Assert.Contains("estuche", ex.Message);
    }

    [Fact]
    public void Build_WithMultipleErrors_ExceptionContainsAllMessages()
    {
        var ex = Assert.Throws<InvalidOperationException>(() =>
            new OrderBuilder().Build());

        Assert.Contains("cliente", ex.Message);
        Assert.Contains("armazón", ex.Message);
        Assert.Contains("cristal", ex.Message);
        Assert.Contains("estuche", ex.Message);
    }

    // ── Cálculo de TotalPrice ────────────────────────────────────────────────

    [Fact]
    public void TotalPrice_IsCorrectSumOfAllComponents()
    {
        // Frame $200 + CristalD ($85+$30) + CristalI ($85+$30) + Estuche $15 + Acc1 $3.50 + Acc2 $3.50
        var order = OrderTestFixture.BuildValidOrder();
        const decimal expected = 200m + (85m + 30m) + (85m + 30m) + 15m + 3.50m + 3.50m;

        Assert.Equal(expected, order.TotalPrice);
    }

    [Fact]
    public void TotalPrice_WithNoAccessories_ExcludesAccessoryCost()
    {
        var order = new OrderBuilder()
            .ForCustomer(OrderTestFixture.DefaultCustomer())
            .WithFrame(OrderTestFixture.DefaultFrame(200m))
            .WithSameCrystals(OrderTestFixture.DefaultCrystal(85m))
            .WithGlassesCase(OrderTestFixture.DefaultGlassesCase(15m))
            .Build();

        Assert.Equal(200m + 85m + 85m + 15m, order.TotalPrice);
    }

    [Fact]
    public void TotalPrice_CrystalPriceIncludesTreatments()
    {
        var crystal = OrderTestFixture.DefaultCrystal(85m);
        crystal.AddTreatment(OrderTestFixture.AntiReflectiveTreatment(30m));
        crystal.AddTreatment(OrderTestFixture.BlueFilterTreatment(20m));

        // TotalPrice del cristal = 85 + 30 + 20 = 135
        Assert.Equal(135m, crystal.TotalPrice);
    }

    // ── Crystal: receta y tratamientos ──────────────────────────────────────

    [Fact]
    public void Crystal_WithoutPrescription_IsNullByDefault()
    {
        var crystal = OrderTestFixture.DefaultCrystal();
        Assert.Null(crystal.Prescription);
    }

    [Fact]
    public void Crystal_WithPrescription_StoresPrescriptionValues()
    {
        var prescription = new OpticalPrescription(-2.50m, -0.75m, 180);
        var crystal = OrderTestFixture.DefaultCrystal(prescription: prescription);

        Assert.Equal(-2.50m, crystal.Prescription!.Sphere);
        Assert.Equal(-0.75m, crystal.Prescription.Cylinder);
        Assert.Equal(180, crystal.Prescription.Axis);
    }

    [Fact]
    public void Crystal_AddTreatment_ReturnsSameInstanceForFluentChaining()
    {
        var crystal = OrderTestFixture.DefaultCrystal();
        var returned = crystal.AddTreatment(OrderTestFixture.AntiReflectiveTreatment());

        Assert.Same(crystal, returned);
        Assert.Single(crystal.Treatments);
    }
}
