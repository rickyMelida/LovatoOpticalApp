using LovatoOpticalApp.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public StateEnum State { get; set; } = StateEnum.Drafts;
        public Customer Customer { get; set; }
        public Frame? Frame { get; set; }
        public Crystal? CrystalLeft { get; set; }
        public Crystal? CrystalRight { get; set; }

        // Orden de trabajo enviada al laboratorio para fabricar los cristales
        public CrystalOrderWork CrystalOrderWork { get; set; }

        // Accesorios opcionales (goma, hilo, paño, etc.)
        [NotMapped]
        public List<Accessory> Accessories { get; set; } = new();

        public string Observations { get; set; }

        [NotMapped]
        public decimal FramePrice => Frame?.SalePrice ?? 0;
        [NotMapped]
        public decimal CrystalPrice => (CrystalLeft?.SalePrice ?? 0) + (CrystalRight?.SalePrice ?? 0);
        [NotMapped]
        public decimal AccessoriesPrice => Accessories.Sum(a => a.SalePrice);
        [NotMapped]
        public decimal TotalPrice => FramePrice + CrystalPrice + AccessoriesPrice;

        public (bool IsValid, List<string> Errors) Validate()
        {
            var errors = new List<string>();

            if (Customer == null)
                errors.Add("El cliente es obligatorio.");

            if (Frame == null)
                errors.Add("El armazón es obligatorio.");

            if (CrystalLeft == null && CrystalRight == null)
                errors.Add("Se requiere al menos un cristal.");

            return (!errors.Any(), errors);
        }

        /// <summary>
        /// Confirma la orden cambiando su estado a <see cref="StateEnum.Confirmed"/>.
        /// Solo puede confirmarse si la orden es válida.
        /// </summary>
        public void Confirm()
        {
            var (isValid, errors) = Validate();
            if (!isValid)
                throw new InvalidOperationException(
                    $"No se puede confirmar la orden:\n{string.Join("\n", errors)}");

            if (State != StateEnum.Drafts)
                throw new InvalidOperationException(
                    $"La orden ya fue procesada (estado actual: {State}).");

            State = StateEnum.Confirmed;
        }
    }
}