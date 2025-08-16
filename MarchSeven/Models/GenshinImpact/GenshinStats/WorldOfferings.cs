using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class WorldOfferings
{
    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
