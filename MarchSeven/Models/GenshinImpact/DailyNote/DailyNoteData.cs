using System.Text.Json.Serialization;

namespace MarchSeven.Models.GenshinImpact.DailyNote;

public class DailyNoteData
{
    [JsonPropertyName("current_resin")]
    public int CurrentResin { get; set; } //現在の樹脂

    [JsonPropertyName("max_resin")]
    public int MaxResin { get; set; } //最大樹脂

    [JsonPropertyName("resin_recovery_time")]
    public string ResinRecoveryTime { get; set; } = string.Empty; //樹脂回復まで

    [JsonPropertyName("finished_task_num")]
    public int FinishedTaskNum { get; set; } //デイリーの完了数

    [JsonPropertyName("total_task_num")]
    public int ToatalTaskNum { get; set; } //デイリーの総数

    [JsonPropertyName("is_extra_task_reward_received")]
    public bool IsExtraTaskRewardRecevied { get; set; } //キャサリンの報酬

    [JsonPropertyName("remain_resin_discount_num")]
    public int RemainResinDiscountNum { get; set; } //週ボス残り

    [JsonPropertyName("resin_discount_num_limit")]
    public int ResionDiscountNumLimit { get; set; } //週ボス総数

    [JsonPropertyName("current_expedition_num")]
    public int CurrentExpeditionNum { get; set; } //探索派遣

    [JsonPropertyName("max_expedition_num")]
    public int MaxExpeditionNum { get; set; } //探索派遣の総数

    [JsonPropertyName("expeditions")]
    public Expedition[]? Expeditions { get; set; } //探索派遣のデータ

    [JsonPropertyName("current_home_coin")]
    public int CurrentHomeCoin { get; set; } //洞天宝銭

    [JsonPropertyName("max_home_coin")]
    public int MaxHomeCoin { get; set; } //洞天宝銭の最大

    [JsonPropertyName("home_coin_recovery_time")]
    public string HomeCoinRecoveryTime { get; set; } = string.Empty; //洞天宝銭の回復まで

    [JsonPropertyName("calendar_url")]
    public string CalenderUrl { get; set; } = string.Empty; //わからん

    [JsonPropertyName("transformer")]
    public Transformer? Transformer { get; set; } //参量物質変化器のデータ
}
