using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Core.ValueObjects
{
    public class CrystalTreatment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public TreatmentTypeEnum Type { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        private CrystalTreatment() { }

        public CrystalTreatment(TreatmentTypeEnum type, decimal price, string description = "")
        {
            Id = Guid.NewGuid();
            Type = type;
            Price = price;
            Description = description;
        }
    }
}
