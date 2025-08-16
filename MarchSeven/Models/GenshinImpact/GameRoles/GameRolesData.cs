using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GameRoles;

public class GameRolesData
{
    [JsonPropertyName("list")]
    public List<GameRole> List { get; set; } = [];
}
