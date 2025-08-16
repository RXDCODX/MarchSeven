using System.Text.Json.Serialization;
using MarchSeven.Models.GenshinImpact;

namespace MarchSeven.Models.ZenlessZoneZero.ZenlessStats;

public class ZenlessStats : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public ZenlessStatsData? Data { get; set; }
}
