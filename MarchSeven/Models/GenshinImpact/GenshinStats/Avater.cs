using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.GenshinStats;

public class Avater
{
    [JsonPropertyName("actived_constellation_num")]
    public int ActivedConstellationNum { get; set; }

    [JsonPropertyName("card_image")]
    public string CardImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("element")]
    public string Element { get; set; } = string.Empty;

    [JsonPropertyName("fetter")]
    public int Fetter { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("is_chosen")]
    public bool IsChosen { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("rearity")]
    public int Rearity { get; set; }
}
