using System.ComponentModel.DataAnnotations;

namespace LovatoOpticalApp.Application.DTOs.Order
{
    public class CreateOrderRequestDto
    {
        [Required] 
        public Guid CustomerId { get; set; }
        [Required] 
        public Guid FrameId { get; set; }
        public Guid? CrystalRightId { get; set; }
        public Guid? CrystalLeftId { get; set; }
        [Required] 
        public Guid GlassesCaseId { get; set; }
        public string? Observations { get; set; }

        [Required] 
        public CrystalOrderWorkRequestDto CrystalOrderWork { get; set; }
    }
}
