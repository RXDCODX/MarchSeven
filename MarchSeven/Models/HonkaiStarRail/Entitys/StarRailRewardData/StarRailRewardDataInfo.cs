using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailRewardData;

public class StarRailRewardDataInfo
{
    [JsonPropertyName("month")]
    public int Month { get; set; }

    [JsonPropertyName("awards")]
    public StarRailAward[]? Awards { get; set; }

    [JsonPropertyName("biz")]
    public string Biz { get; set; } = string.Empty;

    [JsonPropertyName("resign")]
    public bool Resign { get; set; }

    [JsonPropertyName("short_extra_award")]
    public ShortExtraAward? ShortExtraAward { get; set; }
}
