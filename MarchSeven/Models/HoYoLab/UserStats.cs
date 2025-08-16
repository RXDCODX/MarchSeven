using System.Text.Json.Serialization;
using MarchSeven.Models.GenshinImpact;

namespace MarchSeven.Models.HoYoLab;

public class UserStats : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public UserStatsData? Data { get; set; }
}
