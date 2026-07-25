namespace LovatoOpticalApp.Core.Enums
{
    public enum CrystalOrderWorkStateEnum
    {
        Pending,        // Orden creada, aún no enviada al laboratorio
        SentToLab,      // Enviada al laboratorio
        InProduction,   // En proceso de fabricación
        Ready,          // Lista para retirar del laboratorio
        Delivered       // Entregada y cerrada
    }
}
