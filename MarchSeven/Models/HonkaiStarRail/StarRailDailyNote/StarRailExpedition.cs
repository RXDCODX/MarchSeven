using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.StarRailDailyNote;

public class StarRailExpedition
{
    [JsonPropertyName("avatars")]
    public string[]? Avatars { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("remaining_time")]
    public int RemainingTime { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("item_url")]
    public string ItemUrl { get; set; } = string.Empty;

    [JsonPropertyName("finish_ts")]
    public long FinishTimestamp { get; set; }

    /// <summary>
    /// Время завершения экспедиции
    /// </summary>
    [JsonIgnore]
    public DateTime FinishTime => DateTimeOffset.FromUnixTimeSeconds(FinishTimestamp).DateTime;

    /// <summary>
    /// Время завершения экспедиции (с учетом часового пояса)
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset FinishTimeOffset => DateTimeOffset.FromUnixTimeSeconds(FinishTimestamp);

    /// <summary>
    /// Оставшееся время до завершения экспедиции
    /// </summary>
    [JsonIgnore]
    public TimeSpan RemainingTimeSpan => TimeSpan.FromSeconds(RemainingTime);
}
