namespace LovatoOpticalApp.Core.ValueObjects
{
    public class OpticalPrescription
    {
        public decimal Sphere { get; private set; }
        public decimal Cylinder { get; private set; }
        public int Axis { get; private set; }
        public decimal? Addition { get; private set; }

        private OpticalPrescription() { }

        public OpticalPrescription(decimal sphere, decimal cylinder, int axis, decimal? addition = null)
        {
            Sphere = sphere;
            Cylinder = cylinder;
            Axis = axis;
            Addition = addition;
        }

        public override string ToString() =>
            $"Esf: {Sphere:+0.00;-0.00}  Cil: {Cylinder:+0.00;-0.00}  Eje: {Axis}°" +
            (Addition.HasValue ? $"  Add: {Addition:+0.00;-0.00}" : string.Empty);
    }
}
