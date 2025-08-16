using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.Languages;

public class LanguagesData
{
    [JsonPropertyName("langs")]
    public LangData[]? Langs { get; set; }
}
