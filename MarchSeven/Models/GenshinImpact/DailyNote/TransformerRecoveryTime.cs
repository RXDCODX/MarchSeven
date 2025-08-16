using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.DailyNote;

public class TransformerRecoveryTime
{
    public int Day { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }

    [JsonPropertyName("reached")]
    public bool Reached { get; set; }
}
