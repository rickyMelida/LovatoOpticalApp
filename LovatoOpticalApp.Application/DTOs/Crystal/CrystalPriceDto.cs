namespace LovatoOpticalApp.Application.DTOs
{
	public class CrystalPriceDto
	{
	public Guid Id { get; set; }
        public Guid CrystalTypeId { get; set; }
        public decimal Price { get; set; } // En Guaraníes
        public DateTime EffectivityDate { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // Relación
        public CrystalTypeDto? CrystalType { get; set; }
	}
}