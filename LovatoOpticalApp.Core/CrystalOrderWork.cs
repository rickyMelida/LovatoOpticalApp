using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core
{
    public class CrystalOrderWork
    {
        // --- Identidad ---
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public CrystalOrderWorkStateEnum State { get; set; } = CrystalOrderWorkStateEnum.Pending;
        public string Material { get; set; }
        public int Index { get; set; }
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
        public string Observation { get; set; }
    }
}
