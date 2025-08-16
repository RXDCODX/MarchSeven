using System.Text.Json.Serialization;

namespace MarchSeven.Models.HonkaiStarRail.StarRailDailyNote;

public class StarRailDailyNoteData
{
    [JsonPropertyName("current_stamina")]
    public int CurrentStamina { get; set; }

    [JsonPropertyName("max_stamina")]
    public int MaxStamina { get; set; }

    [JsonPropertyName("stamina_recover_time")]
    public int StaminaRecoverTime { get; set; }

    [JsonPropertyName("stamina_full_ts")]
    public long StaminaFullTimestamp { get; set; }

    /// <summary>
    /// Время, когда stamina будет полностью восстановлена
    /// </summary>
    [JsonIgnore]
    public DateTime StaminaFullTime => DateTimeOffset.FromUnixTimeSeconds(StaminaFullTimestamp).DateTime;

    /// <summary>
    /// Время, когда stamina будет полностью восстановлена (с учетом часового пояса)
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset StaminaFullTimeOffset => DateTimeOffset.FromUnixTimeSeconds(StaminaFullTimestamp);

    [JsonPropertyName("accepted_epedition_num")]
    public int AcceptedExpeditionNum { get; set; }

    [JsonPropertyName("total_expedition_num")]
    public int TotalExpeditionNum { get; set; }

    [JsonPropertyName("expeditions")]
    public StarRailExpedition[]? Expeditions { get; set; }

    [JsonPropertyName("current_train_score")]
    public int CurrentTrainScore { get; set; }

    [JsonPropertyName("max_train_score")]
    public int MaxTrainScore { get; set; }

    [JsonPropertyName("current_rogue_score")]
    public int CurrentRogueScore { get; set; }

    [JsonPropertyName("max_rogue_score")]
    public int MaxRogueScore { get; set; }

    [JsonPropertyName("weekly_cocoon_cnt")]
    public int WeeklyCocoonCount { get; set; }

    [JsonPropertyName("weekly_cocoon_limit")]
    public int WeeklyCocoonLimit { get; set; }

    [JsonPropertyName("current_reserve_stamina")]
    public int CurrentReserveStamina { get; set; }

    [JsonPropertyName("is_reserve_stamina_full")]
    public bool IsReserveStaminaFull { get; set; }

    [JsonPropertyName("rogue_tourn_weekly_unlocked")]
    public bool RogueTournWeeklyUnlocked { get; set; }

    [JsonPropertyName("rogue_tourn_weekly_max")]
    public int RogueTournWeeklyMax { get; set; }

    [JsonPropertyName("rogue_tourn_weekly_cur")]
    public int RogueTournWeeklyCurrent { get; set; }

    [JsonPropertyName("current_ts")]
    public long CurrentTimestamp { get; set; }

    /// <summary>
    /// Текущее время сервера
    /// </summary>
    [JsonIgnore]
    public DateTime CurrentTime => DateTimeOffset.FromUnixTimeSeconds(CurrentTimestamp).DateTime;

    /// <summary>
    /// Текущее время сервера (с учетом часового пояса)
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset CurrentTimeOffset => DateTimeOffset.FromUnixTimeSeconds(CurrentTimestamp);

    [JsonPropertyName("rogue_tourn_exp_is_full")]
    public bool RogueTournExpIsFull { get; set; }
}
