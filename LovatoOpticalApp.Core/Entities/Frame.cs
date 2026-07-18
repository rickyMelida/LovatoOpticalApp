using LovatoOpticalApp.Core.Entities.Enums;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Core.Entities
{
    public class Frame : Product
    {
        public string Code { get; private set; }
        public FrameMaterialEnum Material { get; private set; }
        public FrameTypeEnum FrameType { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private Frame() { }

        public Frame(
            string name,
            string code,
            FrameMaterialEnum material,
            FrameTypeEnum frameType,
            string color,
            decimal purchasePrice,
            decimal salePrice,
            int quantity,
            int minimumQuantity = 1)
        {
            Type = ProductTypeEnum.Frame;  // se asigna aquí
            Name = name;
            Code = code;
            Material = material;
            FrameType = frameType;
            Color = color;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Quantity = quantity;
            MinimumQuantity = minimumQuantity;
        }
    }
}