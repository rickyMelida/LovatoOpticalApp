namespace LovatoOpticalApp.Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string State { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FrameName { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? CrystalOrderWorkId { get; set; }
    }
}
