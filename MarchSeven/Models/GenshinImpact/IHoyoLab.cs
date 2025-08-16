using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact;

public interface IHoyoLab
{
    [JsonPropertyName("retcode")]
    public int Retcode { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
