namespace LovatoOpticalApp.Core.Entities
{
	public class CrystalPriceQuoteDetail
	{
		public Guid Id { get; set; }
        public Guid CrystalPriceQuoteId { get; set; }
        public string Eye { get; set; } // "OD" o "OI"
        public Guid CrystalTypeId { get; set; }
        public Guid? CrystalBrandId { get; set; } // Solo para multifocales
        public decimal Spherical { get; set; }
        public decimal Cylindric { get; set; }
        public decimal Addition { get; set; }
        public decimal CrystalPrice { get; set; }
        public decimal CoatingPrice { get; set; }
        public decimal AdditionalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        // Relaciones
        public CrystalPriceQuote CrystalPriceQuote { get; set; }
        public CrystalType CrystalType { get; set; }
        public CrystalBrand CrystalBrand { get; set; }

        // Relación muchos-a-muchos con recubrimientos
        public ICollection<CoatingPriceQuoteDetails> Recubrimientos { get; set; } = new List<CoatingPriceQuoteDetails>();
	}
}