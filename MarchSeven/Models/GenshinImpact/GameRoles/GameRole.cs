using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GameRoles;

public class GameRole
{
    [JsonPropertyName("game_biz")]
    public string GameRegionName { get; set; } = "";

    [JsonPropertyName("region")]
    public string Region { get; set; } = "";

    [JsonPropertyName("game_uid")]
    public string GameUid { get; set; } = "";

    [JsonPropertyName("nickname")]
    public string NickName { get; set; } = "";

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("is_chosen")]
    public bool IsChosen { get; set; }

    [JsonPropertyName("region_name")]
    public string RegionName { get; set; } = "";

    [JsonPropertyName("is_official")]
    public bool IsOfficial { get; set; }
}
