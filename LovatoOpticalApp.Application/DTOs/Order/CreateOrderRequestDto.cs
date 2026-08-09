namespace LovatoOpticalApp.Application.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        public Guid CustomerId { get; set; }
        public Guid FrameId { get; set; }
        public CrystalDtoRequest CrystalRight { get; set; }
        public CrystalDtoRequest CrystalLeft { get; set; }
		public List<Guid>? AccessoryId { get; set; }
        public string? Observations { get; set; }
        public CrystalOrderWorkRequestDto CrystalOrderWork { get; set; }
    }
}
