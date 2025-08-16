using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.DailyNote;

public class DailyNote : IHoyoLab
{
    public int Retcode { get; set; }
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public DailyNoteData? Data { get; set; }

    public static TimeData ToTime(string time_str)
    {
        var time = new TimeSpan(0, 0, int.Parse(time_str));
        var CompletedTime = DateTime.Now + time;

        return new TimeData(time, CompletedTime);
    }
}
