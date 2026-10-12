namespace LovatoOpticalApp.Core.Entities
{
	public class CrystalPriceQuote
	{
		public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string ReferenceNumber { get; set; } // Para impresión
        public decimal TotalER { get; set; }
        public decimal TotalEL { get; set; }
        public decimal TotalAmount { get; set; }
        public bool ISBothEyes { get; set; }
        public string AdditionalNotes { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7); // Vence en 7 días por defecto

        // Relaciones
        public Customer Customer { get; set; }
        public ICollection<CrystalPriceQuoteDetail> Detalles { get; set; } = new List<CrystalPriceQuoteDetail>();
	}
}