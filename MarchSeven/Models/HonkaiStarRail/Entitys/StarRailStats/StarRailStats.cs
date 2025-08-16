using System.Text.Json.Serialization;
using MarchSeven.Models.GenshinImpact;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class StarRailStats : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public StarRailStatsData? Data { get; set; }
}
