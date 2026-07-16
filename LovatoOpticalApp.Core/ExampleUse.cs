using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;
namespace LovatoOpticalApp.Core
{
    public static class ExampleUse
    {
        public static void Create()
        {
            var customer = new Customer("John Doe", "123456789", new DateTime(1990, 5, 15), "123 Main St", "555-1234");

            //var frame = new Frame(FrameMaterialEnum.Acetato, "Ray-Ban RB5154", "Ray-Ban", "", "", "", "", 0, 0, 0);
            var leftCrystal = new Crystal("Blanco Reflex", "+250 -250", 0, 0, 1, 0);

            /*var pedido = new OrderBuilder()
            .ForCustomer(customer)
            .WithFrame(frame)
            .WithRightCrystal(new Crystal
            {
                Type = LensType.Progressive,
                Material = LensMaterial.HighIndex,
                Prescription = new OpticalPrescription { Sphere = -2.50m, Cylinder = -0.75m, Axis = 180 },
                Treatments = new() { new CrystalTreatment { Type = TreatmentType.AntiReflective, Price = 30m } },
                BasePrice = 85.00m
            })
            .ConCristalIzquierdo(new Cristal
            {
                Tipo = TipoLente.Progresivo,
                Material = MaterialLente.HighIndex,
                Graduacion = new GraduacionOptica { Esfera = -1.75m, Cilindro = 0m, Eje = 0 },
                Tratamientos = new() { new TratamientoCristal { Tipo = TipoTratamiento.Antireflejo, Precio = 30m } },
                PrecioBase = 85.00m
            })
            .ConEstuche(new Estuche
            {
                Nombre = "Estuche Rígido Premium",
                Tipo = TipoEstuche.Rigido,
                Precio = 15.00m
            })
            .AgregarAccesorio(new AccesorioAnteojo
            {
                Nombre = "Goma para patillas infantil",
                EsInfantil = false,
                Precio = 3.50m
            })
    .Build();

            Console.WriteLine(pedido.GenerarResumen());
            // TOTAL: $448.50

            */
        }
    }
}
