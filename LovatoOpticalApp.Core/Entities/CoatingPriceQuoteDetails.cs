namespace LovatoOpticalApp.Core.Entities
{
	public class CoatingPriceQuoteDetails
	{
		public Guid Id { get; set; }
        public Guid CrystalPriceQuoteDetailId { get; set; }
        public Guid CoatingTypeId { get; set; }
        public decimal Price { get; set; }

        // Relaciones
        public CrystalPriceQuoteDetail CrystalPriceQuoteDetail { get; set; }
        public CoatingType CoatingType { get; set; }
	}
}