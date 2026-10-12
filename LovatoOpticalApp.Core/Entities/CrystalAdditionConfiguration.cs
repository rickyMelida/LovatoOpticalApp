namespace LovatoOpticalApp.Core.Entities
{
	public class CrystalAdditionConfiguration
	{
		public Guid Id { get; set; }
        public decimal PricePerDiopter { get; set; } // Precio multiplicador por valor de adición
        public DateTime EffectivityDate { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
	}
}