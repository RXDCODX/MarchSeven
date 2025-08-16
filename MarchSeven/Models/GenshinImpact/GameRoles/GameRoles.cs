using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GameRoles;

public class GameRoles : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public GameRolesData? Data { get; set; }
}
