using MarchSeven.Models.Abstractions;
using MarchSeven.Models.Core;
using MarchSeven.Models.GenshinImpact;
using MarchSeven.Models.HonkaiStarRail.Entitys;
using MarchSeven.Models.HonkaiStarRail.Entitys.StarRailRewardData;
using MarchSeven.Models.HonkaiStarRail.Entitys.StarRailStats;
using MarchSeven.Util.Errors;

namespace MarchSeven.Models.HonkaiStarRail;

/// <summary>
/// Honkai Star Rail client for HoyoLab WebAPI
/// </summary>
public class StarRailClient : BaseGameClient
{
    /// <summary>
    /// Менеджер энергии Star Rail
    /// </summary>
    internal StarRailClient(ICookie cookie)
        : base(cookie) { }

    internal StarRailClient(ICookie cookie, ClientData data)
        : base(cookie, data) { }

    /// <summary>
    /// Получить статистику Honkai Star Rail
    /// </summary>
    public async Task<StarRailStats> FetchStarRailStatsAsync(StarRailUser user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Пользователь не может быть null");
        }

        var url =
            _clientData.EndPoints.StarRailStats.Url + $"?server={user.Server}&role_id={user.Uid}";
        var result = await new Wrapper<StarRailStats>(_clientData).FetchData(url, _cookie, true);

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить статистику Star Rail")
            : result;
    }

    /// <summary>
    /// Получить ежедневную заметку для Honkai Star Rail
    /// </summary>
    public async Task<StarRailDailyNote.StarRailDailyNote> FetchDailyNoteAsync(StarRailUser user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Пользователь не может быть null");
        }

        var url =
            _clientData.EndPoints.StarRailDailyNote.Url
            + $"?server={user.Server}&role_id={user.Uid}";
        var result = await new Wrapper<StarRailDailyNote.StarRailDailyNote>(_clientData).FetchData(
            url,
            _cookie,
            true
        );

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить ежедневную заметку")
            : result;
    }

    /// <summary>
    /// Получить ежедневную награду для Honkai Star Rail
    /// </summary>
    public async Task<RewardData> ClaimDailyRewardAsync()
    {
        var data = new RewardData();

        var infoUrl =
            _clientData.EndPoints.StarRailRewardInfo.Url + "&lang=" + _clientData.Language;
        var homeUrl =
            _clientData.EndPoints.StarRailRewardData.Url + "&lang=" + _clientData.Language;
        var claimUrl =
            _clientData.EndPoints.StarRailRewardSign.Url + "&lang=" + _clientData.Language;

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

    /// <summary>
    /// Получить данные о наградах Star Rail
    /// </summary>
    public async Task<StarRailRewardData> GetStarRailRewardDataAsync()
    {
        var url = _clientData.EndPoints.StarRailRewardData.Url + "&lang=" + _clientData.Language;
        var result = await new Wrapper<StarRailRewardData>(_clientData).FetchData(url, _cookie);

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить данные о наградах")
            : result;
    }

    protected override string GetGameBiz()
    {
        return "hkrpg_global";
    }

    protected override int GetGameId()
    {
        return 6; // Honkai Star Rail game ID
    }
}
