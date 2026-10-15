using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Application.DTOs
{
	 public class CrystalTypeDto
    {
		public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty; // "Blanco", "Simple", "Bifocal", etc
        public string Description { get; set; } = String.Empty;
        public bool Active { get; set; } = true;
		public CrystalTypeEnum Type { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<CrystalPriceDto> Prices { get; set; } = new List<CrystalPriceDto>();
    }
}