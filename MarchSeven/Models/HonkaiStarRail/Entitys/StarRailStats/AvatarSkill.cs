using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class AvatarSkill
{
    [JsonPropertyName("skillId")]
    public int SkillId { get; set; }

    [JsonPropertyName("skillName")]
    public string SkillName { get; set; } = string.Empty;

    [JsonPropertyName("skillLevel")]
    public int SkillLevel { get; set; }
}
