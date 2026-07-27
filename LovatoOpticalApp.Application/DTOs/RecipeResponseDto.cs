namespace LovatoOpticalApp.Application.DTOs
{
    public class RecipeResponseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Optometrist { get; set; }
        public DateTime PrescriptionIssueDate { get; set; }

        // Visi
        public string VL_OD_ESF { get; set; }
        public string VL_OD_CIL { get; set; }
        public string VL_OD_EJE { get; set; }

        // Visión Lejana (VL) - Ojo Izquierdo (OI)
        public string VL_OI_ESF { get; set; }
        public string VL_OI_CIL { get; set; }
        public string VL_OI_EJE { get; set; }

        // Visión Cercana (VC) - Ojo Derecho (OD)
        public string VC_OD_ESF { get; set; }
        public string VC_OD_CIL { get; set; }
        public string VC_OD_EJE { get; set; }

        // Visión Cercana (VC) - Ojo Izquierdo (OI)
        public string VC_OI_ESF { get; set; }
        public string VC_OI_CIL { get; set; }
        public string VC_OI_EJE { get; set; }
        public string Adicion { get; set; } = string.Empty;
    }
}
