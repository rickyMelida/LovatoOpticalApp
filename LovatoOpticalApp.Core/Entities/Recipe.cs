namespace LovatoOpticalApp.Core.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime PrescriptionIssueDate { get; set; }
        public string Optometrist { get; set; }

        // Visión Lejana (VL) - Ojo Derecho (OD)
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

        protected Recipe() { }

        public Recipe(
            string vl_od_esf, string vl_od_cil, string vl_od_eje,
            string vl_oi_esf, string vl_oi_cil, string vl_oi_eje,
            string vc_od_esf, string vc_od_cil, string vc_od_eje,
            string vc_oi_esf, string vc_oi_cil, string vc_oi_eje,
            string adicion,
            string optometrist,
            DateTime? prescriptionIssueDate = null
        )
        {
            Id = Guid.NewGuid();
            VL_OD_ESF = vl_od_esf;
            VL_OD_CIL = vl_od_cil;
            VL_OD_EJE = vl_od_eje;
            VL_OI_ESF = vl_oi_esf;
            VL_OI_CIL = vl_oi_cil;
            VL_OI_EJE = vl_oi_eje;
            VC_OD_ESF = vc_od_esf;
            VC_OD_CIL = vc_od_cil;
            VC_OD_EJE = vc_od_eje;
            VC_OI_ESF = vc_oi_esf;
            VC_OI_CIL = vc_oi_cil;
            VC_OI_EJE = vc_oi_eje;
            Adicion = adicion;
            Optometrist = optometrist;
            PrescriptionIssueDate = prescriptionIssueDate ?? DateTime.Today;
        }
    }
}
