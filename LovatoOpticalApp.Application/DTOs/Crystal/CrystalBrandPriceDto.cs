namespace LovatoOpticalApp.Application.DTOs
{
	public class CrystalBrandPriceDto
	{
		public Guid Id { get; set; }
        public Guid CrystalBrandId { get; set; }
        public decimal Price { get; set; } // En Guaraníes
        public DateTime EffectivityDate { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

	}
}