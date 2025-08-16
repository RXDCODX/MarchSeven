using System.Text.Json.Serialization;
using MarchSeven.Models.GenshinImpact;

namespace MarchSeven.Models.HoYoLab.UserAccountInfo;

public class UserAccountInfo : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public AccountInfoData? Data { get; set; }
}
