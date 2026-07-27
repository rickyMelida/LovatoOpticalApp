using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core
{
    /// <summary>
    /// Orden de trabajo enviada al laboratorio para la fabricación de los cristales de un pedido.
    /// Contiene las especificaciones ópticas y físicas necesarias para que el laboratorio procese ambos ojos.
    /// </summary>
    public class CrystalOrderWork
    {
        // --- Identidad ---
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CrystalOrderWorkStateEnum State { get; set; } = CrystalOrderWorkStateEnum.Pending;

        // --- Relación con la Orden ---
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        // --- Cristales referenciados (pueden ser nulos si el ojo no aplica) ---
        public Guid? CrystalRightId { get; set; }
        public Crystal CrystalRight { get; set; }

        public Guid? CrystalLeftId { get; set; }
        public Crystal CrystalLeft { get; set; }

        // --- Especificaciones del material ---
        public string Material { get; set; }
        public string Index { get; set; }

        // --- Tratamientos / instrucciones adicionales para el laboratorio ---
        public string TreatmentNotes { get; set; }

        // --- Graduación Ojo Derecho (OD) ---
        public string OD_ESF { get; set; }
        public string OD_CIL { get; set; }
        public string OD_AXIS { get; set; }
        public string OD_ADD { get; set; }
        public string OD_DNP { get; set; }
        public string OD_HEIGHT { get; set; }

        // --- Graduación Ojo Izquierdo (OI) ---
        public string OI_ESF { get; set; }
        public string OI_CIL { get; set; }
        public string OI_AXIS { get; set; }
        public string OI_ADD { get; set; }
        public string OI_DNP { get; set; }
        public string OI_HEIGHT { get; set; }

        // --- Medidas del armazón (necesarias para el tallado) ---
        public string Mounting { get; set; }
        public string Horizontal { get; set; }
        public string Vertical { get; set; }
        public string MajorDiagonal { get; set; }
        public string Bridge { get; set; }
        public string PantoscopicAngle { get; set; }
        public string PanoramicAngle { get; set; }

        // --- Acceso directo al cliente a través de la Orden ---
        [NotMapped]
        public Entities.Customer Customer => Order?.Customer;
    }
}
