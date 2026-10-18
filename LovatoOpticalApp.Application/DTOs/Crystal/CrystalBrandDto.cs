namespace LovatoOpticalApp.Application.DTOs
{
	public class CrystalBrandDto
	{
		public Guid Id { get; set; }
        public string Name { get; set; } // "Essilor", "Zeiss", etc
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<CrystalBrandPriceDto> Prices { get; set; } = new List<CrystalBrandPriceDto>();
	}
}