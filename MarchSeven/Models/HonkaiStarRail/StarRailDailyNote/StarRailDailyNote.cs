using System.Text.Json.Serialization;
using MarchSeven.Models.GenshinImpact;

namespace MarchSeven.Models.HonkaiStarRail.StarRailDailyNote;

public class StarRailDailyNote : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public StarRailDailyNoteData? Data { get; set; }
}
