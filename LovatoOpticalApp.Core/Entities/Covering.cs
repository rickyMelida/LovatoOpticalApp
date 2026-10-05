using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Core
{
	public class Covering
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public List<Crystal> Crystals { get; set; } = new();

	}
}