using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.DailyNote;

public class Expedition
{
    [JsonPropertyName("avatar_side_icon")]
    public string AvatarSideIconUrl { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("remained_time")]
    public string RemainedTime { get; set; } = string.Empty;
}
