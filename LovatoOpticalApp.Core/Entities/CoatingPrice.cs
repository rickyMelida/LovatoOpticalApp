namespace LovatoOpticalApp.Core.Entities
{
	public class CoatingPrice
	{
		public Guid Id { get; set; }
        public Guid CoatingTypeId { get; set; }
        public decimal Price { get; set; } // En Guaraníes
        public DateTime EffectivityDate { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // Relación
        public CoatingType CoatingType { get; set; }
	}
}