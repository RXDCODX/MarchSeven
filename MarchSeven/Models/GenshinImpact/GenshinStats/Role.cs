using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class Role
{
    public string AvaterUrl { get; set; } = string.Empty;

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("nickname")]
    public string NickName { get; set; } = string.Empty;

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
}
