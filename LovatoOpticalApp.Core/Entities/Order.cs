using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public StateEnum State { get; set; } = StateEnum.Drafts;
        public Customer Customer { get; set; }
        public Frame Frame { get; set; }
        public Crystal CrystalLeft { get; set; }
        public Crystal CrystalRight { get; set; }

        // Orden de trabajo enviada al laboratorio para fabricar los cristales
        public CrystalOrderWork CrystalOrderWork { get; set; }

        // Estuche dedicado (obligatorio según el diagrama)
        [NotMapped]
        public IAccessory GlassesCase { get; set; }

        // Accesorios opcionales (goma, hilo, paño, etc.)
        [NotMapped]
        public List<IAccessory> Accessories { get; set; } = new();

        public string Observations { get; set; }

        [NotMapped]
        public decimal FramePrice => Frame?.SalePrice ?? 0;
        [NotMapped]
        public decimal CrystalPrice => (CrystalLeft?.TotalPrice ?? 0) + (CrystalRight?.TotalPrice ?? 0);
        [NotMapped]
        public decimal GlassesCasePrice => GlassesCase?.SalePrice ?? 0;
        [NotMapped]
        public decimal AccessoriesPrice => Accessories.Sum(a => a.SalePrice);
        [NotMapped]
        public decimal TotalPrice => FramePrice + CrystalPrice + GlassesCasePrice + AccessoriesPrice;

        public (bool IsValid, List<string> Errors) Validate()
        {
            var errors = new List<string>();

            if (Customer == null)
                errors.Add("El cliente es obligatorio.");

            if (Frame == null)
                errors.Add("El armazón es obligatorio.");

            if (CrystalLeft == null && CrystalRight == null)
                errors.Add("Se requiere al menos un cristal.");

            if (GlassesCase == null)
                errors.Add("El estuche es obligatorio.");

            return (!errors.Any(), errors);
        }
    }
}
