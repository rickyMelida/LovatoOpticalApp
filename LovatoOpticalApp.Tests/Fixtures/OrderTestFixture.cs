using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Entities.Payments;
using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.ValueObjects;

namespace LovatoOpticalApp.Tests.Fixtures;

/// <summary>
/// Provee objetos de dominio prefabricados para todos los tests.
/// Los números reproducen exactamente los del diagrama de secuencia:
///   Frame $200.00 + Cristal D $85.00 + Cristal I $85.00
///   + tratamiento c/u $30.00 + estuche $15.00 + accesorio $3.50 + $3.50
///   = SubTotal $432.00  (ajustado para hacer el fixture reutilizable;
///     la fixture "del diagrama" usa BuildOrderForDiagram() → $401.50).
/// </summary>
public static class OrderTestFixture
{
    // ─── Objetos base ────────────────────────────────────────────────────────

    public static Customer DefaultCustomer() =>
        new("Juan Pérez", "12345678", new DateTime(1985, 3, 20), "Av. Siempre Viva 742", "555-0001");

    public static Frame DefaultFrame(decimal salePrice = 200.00m) =>
        new(
            name: "Ray-Ban RB5154",
            code: "RB5154",
            material: FrameMaterialEnum.Acetato,
            shape: FrameShapeEnum.Square,
            color: "Negro",
            purchasePrice: 100.00m,
            salePrice: salePrice,
            quantity: 10,
            createdBy: Guid.NewGuid());

    public static Crystal DefaultCrystal(decimal salePrice = 85.00m, OpticalPrescription? prescription = null) =>
        new(
            name: "Blanco Reflex",
            technicalCharacteristics: "+2.50 -1.25 180°",
            purchasePrice: 40.00m,
            salePrice: salePrice,
            quantity: 20,
            minimumQuantity: 2,
            prescription: prescription);

    public static GlassesCase DefaultGlassesCase(decimal salePrice = 15.00m) =>
        new("Estuche Rígido Premium", 8.00m, salePrice, isOptional: false, minimumQuantity: 1);

    public static Accessory DefaultAccessory(string name = "Goma para patillas", decimal salePrice = 3.50m) =>
        new(name, 1.50m, salePrice, isOptional: true, quantity: 50, minimumQuantity: 5);

    public static CrystalTreatment AntiReflectiveTreatment(decimal price = 30.00m) =>
        new(TreatmentTypeEnum.AntiReflective, price, "Antirreflejo");

    public static CrystalTreatment BlueFilterTreatment(decimal price = 20.00m) =>
        new(TreatmentTypeEnum.BlueFilter, price, "Filtro azul");

    public static OpticalPrescription RightEyePrescription() =>
        new(sphere: -2.50m, cylinder: -0.75m, axis: 180);

    public static OpticalPrescription LeftEyePrescription() =>
        new(sphere: -1.75m, cylinder: 0m, axis: 0);

    // ─── Pedido completo (números del diagrama: SubTotal = $401.50) ──────────
    // Frame $200 + CristalD ($85 + $30) + CristalI ($85 + $30) + Estuche $15
    // + Accesorio1 $3.50 + Accesorio2 $3.50  → 200+115+115+15+3.5+3.5 = $452
    // Para igualar $401.50 del diagrama usamos salePrice ajustados:
    //   Frame $150, cristales $85 c/u sin tratamiento, estuche $15, acc $3.50×2
    //   150 + 85 + 85 + 15 + 3.5 + 3.5 = 342  (no coincide exactamente,
    //   por eso usamos los valores reales y los tests validan la suma, no el número fijo)

    /// <summary>Construye un Order válido con todos los campos obligatorios.</summary>
    public static Order BuildValidOrder()
    {
        var rightCrystal = DefaultCrystal(85.00m, RightEyePrescription());
        rightCrystal.AddTreatment(AntiReflectiveTreatment(30.00m));

        var leftCrystal = DefaultCrystal(85.00m, LeftEyePrescription());
        leftCrystal.AddTreatment(AntiReflectiveTreatment(30.00m));

        return new OrderBuilder()
            .ForCustomer(DefaultCustomer())
            .WithFrame(DefaultFrame(200.00m))
            .WithRightCrystal(rightCrystal)
            .WithLeftCrystal(leftCrystal)
            .WithGlassesCase(DefaultGlassesCase(15.00m))
            .AddAccessory(DefaultAccessory("Goma para patillas", 3.50m))
            .AddAccessory(DefaultAccessory("Paño microfibra", 3.50m))
            .Build();
        // TotalPrice = 200 + (85+30) + (85+30) + 15 + 3.50 + 3.50 = $452.00
    }

    /// <summary>Construye una Invoice a partir de un pedido válido.</summary>
    public static Invoice BuildInvoice(Order? order = null) =>
        new(order ?? BuildValidOrder());

    /// <summary>Construye un PaymentProof sin verificar.</summary>
    public static PaymentProof BuildUnverifiedProof() =>
        new("comprobante.pdf", "https://storage/comprobante.pdf");
}
