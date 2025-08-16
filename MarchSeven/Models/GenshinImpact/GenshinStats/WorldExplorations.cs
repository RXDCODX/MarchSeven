using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class WorldExplorations
{
    [JsonPropertyName("background_image")]
    public string BackgroundImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("cover")]
    public string CoverUrl { get; set; } = string.Empty;

    [JsonPropertyName("exploration_percentage")]
    public int ExplorationPercentage { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("inner_icon")]
    public string InnerIcon { get; set; } = string.Empty;

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("map_url")]
    public string MapUrl { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("offerings")]
    public WorldOfferings[]? Offerings { get; set; }

    [JsonPropertyName("parent_id")]
    public int ParentId { get; set; }

    [JsonPropertyName("strategy_url")]
    public string StrategyUrl { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}
