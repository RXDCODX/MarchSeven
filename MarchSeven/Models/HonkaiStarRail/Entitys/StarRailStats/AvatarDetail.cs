using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class AvatarDetail
{
    [JsonPropertyName("avatarId")]
    public int AvatarId { get; set; }

    [JsonPropertyName("avatarName")]
    public string AvatarName { get; set; } = string.Empty;

    [JsonPropertyName("avatarLevel")]
    public int AvatarLevel { get; set; }

    [JsonPropertyName("avatarElement")]
    public string AvatarElement { get; set; } = string.Empty;

    [JsonPropertyName("avatarRarity")]
    public int AvatarRarity { get; set; }

    [JsonPropertyName("avatarPromotion")]
    public int AvatarPromotion { get; set; }

    [JsonPropertyName("avatarSkillList")]
    public AvatarSkill[]? AvatarSkillList { get; set; }

    [JsonPropertyName("avatarRelicList")]
    public AvatarRelic[]? AvatarRelicList { get; set; }
}
