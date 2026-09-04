using System.Text.Json.Serialization;

namespace LovatoOpticalApp.Application.DTOs.Order
{
    public class CrystalOrderWorkResponseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string State { get; set; } = string.Empty;

        public string? Material { get; set; }
        public int Index { get; set; }
        public string? TreatmentNotes { get; set; }

        [JsonPropertyName("od_ESF")]    public string? OD_ESF { get; set; }
        [JsonPropertyName("od_CIL")]    public string? OD_CIL { get; set; }
        [JsonPropertyName("od_AXIS")]   public string? OD_AXIS { get; set; }
        [JsonPropertyName("od_ADD")]    public string? OD_ADD { get; set; }
        [JsonPropertyName("od_DNP")]    public string? OD_DNP { get; set; }
        [JsonPropertyName("od_HEIGHT")] public string? OD_HEIGHT { get; set; }

        [JsonPropertyName("oi_ESF")]    public string? OI_ESF { get; set; }
        [JsonPropertyName("oi_CIL")]    public string? OI_CIL { get; set; }
        [JsonPropertyName("oi_AXIS")]   public string? OI_AXIS { get; set; }
        [JsonPropertyName("oi_ADD")]    public string? OI_ADD { get; set; }
        [JsonPropertyName("oi_DNP")]    public string? OI_DNP { get; set; }
        [JsonPropertyName("oi_HEIGHT")] public string? OI_HEIGHT { get; set; }

        public string? Mounting { get; set; }
        public string? Horizontal { get; set; }
        public string? Vertical { get; set; }
        public string? MajorDiagonal { get; set; }
        public string? Bridge { get; set; }
        public string? PantoscopicAngle { get; set; }
        public string? PanoramicAngle { get; set; }
        public string? Observation { get; set; }
    }
}
