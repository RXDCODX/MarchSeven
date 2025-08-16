using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class Home
{
    [JsonPropertyName("comfort_level_icon")]
    public string ComfortLevelIconUrl { get; set; } = string.Empty;

    [JsonPropertyName("comfort_level_name")]
    public string ComfortLevelName { get; set; } = string.Empty;

    [JsonPropertyName("comfort_num")]
    public int ComfortNum { get; set; }

    [JsonPropertyName("icon")]
    public string IconUrl { get; set; } = string.Empty;

    [JsonPropertyName("item_num")]
    public int ItemNum { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("visit_num")]
    public int VisitNum { get; set; }
}
