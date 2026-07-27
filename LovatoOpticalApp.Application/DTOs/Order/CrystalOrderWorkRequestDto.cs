using System.ComponentModel.DataAnnotations;

namespace LovatoOpticalApp.Application.DTOs.Order
{
    public class CrystalOrderWorkRequestDto
    {
        public string Material { get; set; }
        public string Index { get; set; }
        public string? TreatmentNotes { get; set; }

        // Graduación Ojo Derecho (OD)
        public string? OD_ESF { get; set; }
        public string? OD_CIL { get; set; }
        public string? OD_AXIS { get; set; }
        public string? OD_ADD { get; set; }
        public string? OD_DNP { get; set; }
        public string? OD_HEIGHT { get; set; }

        // Graduación Ojo Izquierdo (OI)
        public string? OI_ESF { get; set; }
        public string? OI_CIL { get; set; }
        public string? OI_AXIS { get; set; }
        public string? OI_ADD { get; set; }
        public string? OI_DNP { get; set; }
        public string? OI_HEIGHT { get; set; }

        // Medidas del armazón
        public string Mounting { get; set; }
        public string Horizontal { get; set; }
        public string Vertical { get; set; }
        public string MajorDiagonal { get; set; }
        public string Bridge { get; set; }
        public string? PantoscopicAngle { get; set; }
        public string? PanoramicAngle { get; set; }
        public string? Observations { get; set; }
    }
}
