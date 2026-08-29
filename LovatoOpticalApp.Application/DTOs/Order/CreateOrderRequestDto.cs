using System.ComponentModel.DataAnnotations;

namespace LovatoOpticalApp.Application.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        [Required] 
        public Guid CustomerId { get; set; }
        public Guid FrameId { get; set; }
        public Guid? CrystalRightId { get; set; }
        public Guid? CrystalLeftId { get; set; }
        public string? Observations { get; set; }

        [Required] 
        public CrystalOrderWorkRequestDto CrystalOrderWork { get; set; }
    }
}
