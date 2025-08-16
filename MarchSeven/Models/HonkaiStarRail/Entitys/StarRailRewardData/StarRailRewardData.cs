using System.Text.Json.Serialization;
using MarchSeven.Models.GenshinImpact;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailRewardData;

public class StarRailRewardData : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public StarRailRewardDataInfo? Data { get; set; }
}
