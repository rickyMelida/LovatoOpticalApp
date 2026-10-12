using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Core
{
	public class CoatingType
	{
		public Guid Id { get; set; }
        public string Name { get; set; } // "Blanco", "Antirreflex", etc
        public string Code { get; set; } // "blanco", "antirreflex"
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<CoatingPrice> Prices { get; set; } = new List<CoatingPrice>();

	}
}