using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.Languages;

public class Languages : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public LanguagesData? Data { get; set; }
}
