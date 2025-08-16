using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class StarRailStatsData
{
    [JsonPropertyName("detailInfo")]
    public DetailInfo? DetailInfo { get; set; }

    [JsonPropertyName("avatarDetailList")]
    public AvatarDetail[]? AvatarDetailList { get; set; }

    [JsonPropertyName("worldDetailList")]
    public WorldDetail[]? WorldDetailList { get; set; }
}
