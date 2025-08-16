using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class Stats
{
    [JsonPropertyName("achievement_number")]
    public int Achievements { get; set; }

    [JsonPropertyName("active_day_number")]
    public int ActiveDays { get; set; }

    [JsonPropertyName("anemoculus_number")]
    public int Anemoculus { get; set; }

    [JsonPropertyName("dendroculus_number")]
    public int Dendroculus { get; set; }

    [JsonPropertyName("electroculus_number")]
    public int Electroculs { get; set; }

    [JsonPropertyName("geoculus_number")]
    public int Geoculus { get; set; }

    [JsonPropertyName("avatar_number")]
    public int Avaters { get; set; }

    [JsonPropertyName("common_chest_number")]
    public int CommonChestNumber { get; set; }

    [JsonPropertyName("exquisite_chest_number")]
    public int ExquisiteChestNumber { get; set; }

    [JsonPropertyName("luxurious_chest_number")]
    public int LuxuriousChestNumber { get; set; }

    [JsonPropertyName("magic_chest_number")]
    public int MagicChestNumber { get; set; }

    [JsonPropertyName("precious_chest_number")]
    public int PreciousChestNumber { get; set; }

    [JsonPropertyName("domain_number")]
    public int Domains { get; set; }

    [JsonPropertyName("spiral_abyss")]
    public string SpiralAbyss { get; set; } = string.Empty;

    [JsonPropertyName("way_point_number")]
    public int WayPointNumber { get; set; }
}
