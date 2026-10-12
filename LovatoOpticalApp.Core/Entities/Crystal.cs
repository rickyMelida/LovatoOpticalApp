using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Core.Entities
{
    public class CrystalType
    {
		public Guid Id { get; set; }
        public string Name { get; set; } // "Blanco", "Simple", "Bifocal", etc
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<CrystalPrice> Prices { get; set; } = new List<CrystalPrice>();
    }
}