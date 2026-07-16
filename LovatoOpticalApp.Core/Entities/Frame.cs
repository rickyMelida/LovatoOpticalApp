using LovatoOpticalApp.Core.Entities.Enums;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Core.Entities
{
    public class Frame : Product
    {
        public string Code { get; private set; }
        public FrameMaterialEnum Material { get; private set; }
        public FrameShapeEnum Shape { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Frame(
            string name,
            string code,
            FrameMaterialEnum material,
            FrameShapeEnum shape,
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
            Shape = shape;
            Color = color;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Quantity = quantity;
            MinimumQuantity = minimumQuantity;
        }
    }
}