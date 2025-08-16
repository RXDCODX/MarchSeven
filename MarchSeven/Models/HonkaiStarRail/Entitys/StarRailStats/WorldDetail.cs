using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;

public class WorldDetail
{
    [JsonPropertyName("worldId")]
    public int WorldId { get; set; }

    [JsonPropertyName("worldName")]
    public string WorldName { get; set; } = string.Empty;

    [JsonPropertyName("worldLevel")]
    public int WorldLevel { get; set; }

    [JsonPropertyName("worldProgress")]
    public int WorldProgress { get; set; }
}
