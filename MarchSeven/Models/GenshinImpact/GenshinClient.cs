using MarchSeven.Models.Abstractions;
using MarchSeven.Models.Core;
using MarchSeven.Models.GenshinImpact.Entitys;
using MarchSeven.Util.Errors;

namespace MarchSeven.Models.GenshinImpact;

/// <summary>
/// Genshin Impact client for HoyoLab WebAPI
/// </summary>
public class GenshinClient : BaseGameClient
{
    internal GenshinClient(ICookie cookie)
        : base(cookie) { }

    internal GenshinClient(ICookie cookie, ClientData data)
        : base(cookie, data) { }

    /// <summary>
    /// Получить статистику Genshin Impact
    /// </summary>
    public async Task<GenshinStats.GenshinStats> FetchGenshinStatsAsync(GenshinUser user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Пользователь не может быть null");
        }

        var url =
            _clientData.EndPoints.GenshinStats.Url + $"?server={user.Server}&role_id={user.Uid}";
        var result = await new Wrapper<GenshinStats.GenshinStats>(_clientData).FetchData(
            url,
            _cookie,
            true
        );

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить статистику Genshin Impact")
            : result;
    }

    /// <summary>
    /// Получить ежедневную заметку для Genshin Impact
    /// </summary>
    public async Task<DailyNote.DailyNote> FetchDailyNoteAsync(GenshinUser user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Пользователь не может быть null");
        }

        var url =
            _clientData.EndPoints.GenshinDailyNote.Url
            + $"?server={user.Server}&role_id={user.Uid}";
        var result = await new Wrapper<DailyNote.DailyNote>(_clientData).FetchData(
            url,
            _cookie,
            true
        );

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить ежедневную заметку")
            : result;
    }

    /// <summary>
    /// Получить ежедневную награду для Genshin Impact
    /// </summary>
    public async Task<RewardData> ClaimDailyRewardAsync()
    {
        var data = new RewardData();

        var infoUrl = _clientData.EndPoints.GenshinRewardInfo.Url + "&lang=" + _clientData.Language;
        var homeUrl = _clientData.EndPoints.GenshinRewardData.Url + "&lang=" + _clientData.Language;
        var claimUrl =
            _clientData.EndPoints.GenshinRewardSign.Url + "&lang=" + _clientData.Language;

        // Получаем общее количество дней подписи
        var info = await FetchDynamicApiAsync(infoUrl, false);
        var days =
            info?["data"]?["total_sign_day"]?.GetValue<int>()
            ?? throw new InvalidOperationException("Не удалось получить количество дней подписи");
        days--; // Вычитаем 1 из общего количества дней

        // Получаем название и количество предмета из дома
        var home = await FetchDynamicApiAsync(homeUrl, false);
        var name =
            home?["data"]?["awards"]?[days]?["name"]?.GetValue<string>()
            ?? throw new InvalidOperationException("Не удалось получить название награды");
        var amount =
            home?["data"]?["awards"]?[days]?["cnt"]?.GetValue<int>()
            ?? throw new InvalidOperationException("Не удалось получить количество награды");

        data.RewardName = name;
        data.Amount = amount;

        // Получаем награду
        var sign = await FetchDynamicApiAsync(claimUrl, true);
        var code =
            sign?["retcode"]?.GetValue<int>()
            ?? throw new InvalidOperationException("Не удалось получить код ответа");

        if (code == 0)
        {
            data.IsSuccessed = true;
        }
        else if (code == -5003)
        {
            throw new DailyRewardAlreadyReceivedException();
        }
        else if (sign?["data"]?["gt_result"]?["is_risk"]?.GetValue<string>() == "true")
        {
            throw new HoyoLabCaptchaBlockException();
        }
        else
        {
            var message = sign?["message"]?.GetValue<string>() ?? "Неизвестная ошибка";
            throw new HoyoLabApiBadRequestException(message, code);
        }

        return data;
    }

    protected override string GetGameBiz()
    {
        return "hk4e_global";
    }

    protected override int GetGameId()
    {
        return 2; // Genshin Impact game ID
    }

    public static GenshinClient Create(ICookie cookie, ClientData? data = null)
    {
        return data == null ? new GenshinClient(cookie) : new GenshinClient(cookie, data);
    }
}
