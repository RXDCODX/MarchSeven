using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.Languages;

public class LangData
{
    [JsonPropertyName("alias")]
    public string[]? Alias { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
