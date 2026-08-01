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
        public Accessory GlassesCase { get; set; }

        // Accesorios opcionales (goma, hilo, paño, etc.)
        [NotMapped]
        public List<Accessory> Accessories { get; set; } = new();

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

        /// <summary>
        /// Genera la <see cref="CrystalOrderWork"/> para enviar al laboratorio.
        /// Pre-llena los campos ópticos desde las prescripciones de los cristales si están disponibles.
        /// Solo se puede generar si la orden está confirmada.
        /// </summary>
        public CrystalOrderWork GenerateCrystalOrderWork()
        {
            if (State == StateEnum.Drafts)
                throw new InvalidOperationException(
                    "Confirma la orden antes de generar la orden de trabajo para el laboratorio.");

            var work = new CrystalOrderWork
            {
                OrderId       = Id,
                Order         = this,
                CrystalRightId = CrystalRight?.Id,
                CrystalRight   = CrystalRight,
                CrystalLeftId  = CrystalLeft?.Id,
                CrystalLeft    = CrystalLeft,
            };

            // Pre-llenado desde prescripción del ojo derecho
            if (CrystalRight?.Prescription is { } pr)
            {
                work.OD_ESF  = pr.Sphere.ToString("+0.00;-0.00");
                work.OD_CIL  = pr.Cylinder.ToString("+0.00;-0.00");
                work.OD_AXIS = pr.Axis.ToString();
                work.OD_ADD  = pr.Addition?.ToString("+0.00;-0.00") ?? string.Empty;
            }

            // Pre-llenado desde prescripción del ojo izquierdo
            if (CrystalLeft?.Prescription is { } pl)
            {
                work.OI_ESF  = pl.Sphere.ToString("+0.00;-0.00");
                work.OI_CIL  = pl.Cylinder.ToString("+0.00;-0.00");
                work.OI_AXIS = pl.Axis.ToString();
                work.OI_ADD  = pl.Addition?.ToString("+0.00;-0.00") ?? string.Empty;
            }

            CrystalOrderWork = work;
            return work;
        }
    }
}
