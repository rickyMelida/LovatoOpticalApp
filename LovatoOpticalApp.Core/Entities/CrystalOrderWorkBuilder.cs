namespace LovatoOpticalApp.Core.Entities
{
    /// <summary>
    /// Builder fluido para completar los datos de la <see cref="CrystalOrderWork"/>
    /// que se envía al laboratorio.  Se obtiene llamando a <see cref="Order.GenerateCrystalOrderWork"/>,
    /// que pre-llena automáticamente las graduaciones desde las prescripciones de los cristales.
    /// Los métodos de este builder permiten ajustar o agregar los datos físicos del armazón
    /// e instrucciones adicionales al laboratorio.
    /// </summary>
    public class CrystalOrderWorkBuilder
    {
        private readonly CrystalOrderWork _work;

        public CrystalOrderWorkBuilder(CrystalOrderWork work)
        {
            _work = work ?? throw new ArgumentNullException(nameof(work));
        }

        // --- Material y tratamientos ---

        public CrystalOrderWorkBuilder WithMaterial(string material, string index)
        {
            _work.Material = material;
            _work.Index    = index;
            return this;
        }

        public CrystalOrderWorkBuilder WithTreatmentNotes(string notes)
        {
            _work.TreatmentNotes = notes;
            return this;
        }

        // --- Graduación Ojo Derecho (OD) ---

        public CrystalOrderWorkBuilder WithRightEye(
            string esf, string cil, string axis,
            string add = "", string dnp = "", string height = "")
        {
            _work.OD_ESF    = esf;
            _work.OD_CIL    = cil;
            _work.OD_AXIS   = axis;
            _work.OD_ADD    = add;
            _work.OD_DNP    = dnp;
            _work.OD_HEIGHT = height;
            return this;
        }

        // --- Graduación Ojo Izquierdo (OI) ---

        public CrystalOrderWorkBuilder WithLeftEye(
            string esf, string cil, string axis,
            string add = "", string dnp = "", string height = "")
        {
            _work.OI_ESF    = esf;
            _work.OI_CIL    = cil;
            _work.OI_AXIS   = axis;
            _work.OI_ADD    = add;
            _work.OI_DNP    = dnp;
            _work.OI_HEIGHT = height;
            return this;
        }

        // --- Medidas del armazón ---

        public CrystalOrderWorkBuilder WithFrameMeasurements(
            string mounting,
            string horizontal,
            string vertical,
            string majorDiagonal,
            string bridge,
            string pantoscopicAngle = "",
            string panoramicAngle  = "")
        {
            _work.Mounting         = mounting;
            _work.Horizontal       = horizontal;
            _work.Vertical         = vertical;
            _work.MajorDiagonal    = majorDiagonal;
            _work.Bridge           = bridge;
            _work.PantoscopicAngle = pantoscopicAngle;
            _work.PanoramicAngle   = panoramicAngle;
            return this;
        }

        /// <summary>
        /// Devuelve la <see cref="CrystalOrderWork"/> lista para ser enviada al laboratorio.
        /// </summary>
        public CrystalOrderWork Build() => _work;
    }
}
