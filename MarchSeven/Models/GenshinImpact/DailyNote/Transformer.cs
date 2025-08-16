using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.DailyNote;

public class Transformer
{
    [JsonPropertyName("obtained")]
    public bool Obtained { get; set; }

    [JsonPropertyName("recovery_time")]
    public TransformerRecoveryTime? RecoveryTime { get; set; }

    [JsonPropertyName("wiki")]
    public string Wiki { get; set; } = string.Empty;

    [JsonPropertyName("noticed")]
    public bool Noticed { get; set; }

    [JsonPropertyName("latest_job_id")]
    public string LatestJobId { get; set; } = string.Empty;
}
