using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class AvatarRelic
{
    [JsonPropertyName("relicId")]
    public int RelicId { get; set; }

    [JsonPropertyName("relicName")]
    public string RelicName { get; set; } = string.Empty;

    [JsonPropertyName("relicLevel")]
    public int RelicLevel { get; set; }

    [JsonPropertyName("relicRarity")]
    public int RelicRarity { get; set; }
}
