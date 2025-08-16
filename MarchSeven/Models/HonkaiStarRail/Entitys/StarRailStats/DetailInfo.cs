using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class DetailInfo
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonPropertyName("nickname")]
    public string Nickname { get; set; } = string.Empty;

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("worldLevel")]
    public int WorldLevel { get; set; }

    [JsonPropertyName("friendCount")]
    public int FriendCount { get; set; }

    [JsonPropertyName("avatarDetailList")]
    public AvatarDetail[]? AvatarDetailList { get; set; }

    [JsonPropertyName("worldDetailList")]
    public WorldDetail[]? WorldDetailList { get; set; }
}
