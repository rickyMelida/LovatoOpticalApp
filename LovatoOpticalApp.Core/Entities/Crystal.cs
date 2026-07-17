using LovatoOpticalApp.Core.Interfaces;
using LovatoOpticalApp.Core.ValueObjects;

namespace LovatoOpticalApp.Core.Entities
{
    public class Crystal : IProduct
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string TechnicalCharacteristics { get; set; }
        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public int Quantity { get; private set; } = 0;
        public int MinimumQuantity { get; private set; }

        // Graduación óptica (null = lente sin receta)
        public OpticalPrescription? Prescription { get; private set; }

        // Tratamientos aplicados (antirreflejo, filtro azul, etc.)
        public List<CrystalTreatment> Treatments { get; private set; } = new();

        // Precio total = SalePrice base + suma de tratamientos
        public decimal TotalPrice => SalePrice + Treatments.Sum(t => t.Price);

        private Crystal() { }

        public Crystal(string name, string technicalCharacteristics, decimal purchasePrice, decimal salePrice, int quantity, int minimumQuantity, OpticalPrescription? prescription = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            TechnicalCharacteristics = technicalCharacteristics;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Quantity = quantity;
            MinimumQuantity = minimumQuantity;
            Prescription = prescription;
        }

        public Crystal AddTreatment(CrystalTreatment treatment)
        {
            Treatments.Add(treatment);
            return this;
        }
    }
}
