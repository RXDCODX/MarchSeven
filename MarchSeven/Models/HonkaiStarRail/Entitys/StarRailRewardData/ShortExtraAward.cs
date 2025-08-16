using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailRewardData;

public class ShortExtraAward
{
    [JsonPropertyName("has_extra_award")]
    public bool HasExtraAward { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; } = string.Empty;

    [JsonPropertyName("end_time")]
    public string EndTime { get; set; } = string.Empty;

    [JsonPropertyName("list")]
    public object[]? List { get; set; }

    [JsonPropertyName("start_timestamp")]
    public string StartTimestamp { get; set; } = string.Empty;

    [JsonPropertyName("end_timestamp")]
    public string EndTimestamp { get; set; } = string.Empty;
}
