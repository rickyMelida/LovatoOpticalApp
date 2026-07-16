using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public StateEnum State { get; set; } = StateEnum.Drafts;
        public Customer Customer { get; set; }
        public Frame Frame { get; set; }
        public Crystal CrystalLeft { get; set; }
        public Crystal CrystalRight { get; set; }
        public List<IAccessory> Accessories { get; set; } = new();
        public string Observations { get; set; }
        public decimal FramePrice => Frame?.SalePrice ?? 0;
        public decimal CrystalPrice => (CrystalLeft?.SalePrice ?? 0) + (CrystalRight?.SalePrice ?? 0);
        public decimal AccessoriesPrice => Accessories.Sum(a => a.SalePrice);
        public decimal TotalPrice => FramePrice + CrystalPrice + AccessoriesPrice;

        public (bool IsValid, List<string> Errors) Validate()
        {
            var errors = new List<string>();

            if (Frame == null)
                errors.Add("El armazón es obligatorio.");

            if (CrystalLeft == null && CrystalRight == null)
                errors.Add("Se requiere al menos un cristal.");

            if (Accessories == null || !Accessories.Any())
                errors.Add("Se requiere al menos un accesorio.");

            return (!errors.Any(), errors);
        }


    }
}
