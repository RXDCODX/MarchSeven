using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class GenshinStatsData
{
    [JsonPropertyName("avaters")]
    public Avater[]? Avaters { get; set; }

    //[JsonPropertyName("city_explorations")]
    //public Object[]? CityExplorations { get; set; } //不明
    [JsonPropertyName("homes")]
    public Home[]? Homes { get; set; }

    [JsonPropertyName("role")]
    public Role? Role { get; set; }

    [JsonPropertyName("stats")]
    public Stats? Stats { get; set; }
}
