using System.Text.Json.Nodes;
using MarchSeven.Models.Core;
using MarchSeven.Models.GenshinImpact.GameRoles;
using MarchSeven.Models.GenshinImpact.Languages;
using MarchSeven.Models.HoYoLab;
using MarchSeven.Models.HoYoLab.UserAccountInfo;

namespace MarchSeven.Models.Abstractions;

/// <summary>
/// Базовый класс для всех игровых клиентов HoyoLab WebAPI
/// </summary>
public abstract class BaseGameClient(ICookie cookie, ClientData data) : IGameClient
{
    protected readonly ICookie _cookie = cookie ?? throw new ArgumentNullException(nameof(cookie));
    protected readonly ClientData _clientData =
        data ?? throw new ArgumentNullException(nameof(data));

    // Автоматически загруженные данные
    public UserStats? UserStats { get; private set; }
    public UserAccountInfo? AccountInfo { get; private set; }
    public GameRoles? GameRoles { get; private set; }

    protected BaseGameClient(ICookie cookie)
        : this(cookie, new ClientData()) { }

    /// <summary>
    /// Инициализировать клиент с автоматической загрузкой данных
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            // Загружаем основную информацию пользователя
            UserStats = await FetchUserStatsAsync();
            AccountInfo = await GetUserAccountInfoByLTokenAsync();
            GameRoles = await GetGameRolesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ошибка инициализации клиента: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Получить статистику пользователя
    /// </summary>
    public async Task<UserStats> FetchUserStatsAsync(string? uid = null)
    {
        var targetUid = uid ?? _cookie.GetHoyoUid();
        var url = _clientData.EndPoints.UserStats.Url + $"?uid={targetUid}";
        var result = await new Wrapper<UserStats>(_clientData).FetchData(url, _cookie);

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить статистику пользователя")
            : result;
    }

    /// <summary>
    /// Получить информацию об аккаунте пользователя по LToken
    /// </summary>
    public async Task<UserAccountInfo> GetUserAccountInfoByLTokenAsync()
    {
        var url = _clientData.EndPoints.UserAccountInfo.Url;
        var result = await new Wrapper<UserAccountInfo>(_clientData).FetchData(url, _cookie);

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить информацию об аккаунте")
            : result;
    }

    /// <summary>
    /// Получить игровые роли пользователя
    /// </summary>
    public async Task<GameRoles> GetGameRolesAsync(bool isGameOnly = true, string region = "")
    {
        var url = _clientData.EndPoints.GetRoles.Url;
        url += isGameOnly ? $"&game_biz={GetGameBiz()}" : "";
        url += !string.IsNullOrEmpty(region) ? $"&region={region}" : "";
        var result = await new Wrapper<GameRoles>(_clientData).FetchData(url, _cookie);

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить игровые роли")
            : result;
    }

    /// <summary>
    /// Получить доступные языки
    /// </summary>
    public static async Task<Languages> GetAvailableLanguagesAsync()
    {
        var data = new ClientData();
        var result = await new Wrapper<Languages>(data).FetchData(data.EndPoints.GetLanguage.Url);

        return result?.Data == null
            ? throw new InvalidOperationException("Не удалось получить список языков")
            : result;
    }

    /// <summary>
    /// Получить игровой бизнес-идентификатор для API вызовов
    /// </summary>
    protected abstract string GetGameBiz();

    /// <summary>
    /// Получить ID игры для фильтрации ролей
    /// </summary>
    protected abstract int GetGameId();

    /// <summary>
    /// Получить данные через динамический API
    /// </summary>
    protected async Task<JsonNode> FetchDynamicApiAsync(string url, bool isPost = false)
    {
        HttpResponseMessage res;
        if (!isPost)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            req.Headers.Add("x-rpc-app_version", "1.5.0");
            req.Headers.Add("x-rpc-client_type", "5");
            req.Headers.Add("x-rpc-language", _clientData.Language);
            req.Headers.Add("user-agent", _clientData.UserAgent);
            req.Headers.Add("Cookie", _cookie.GetCookie());

            // Добавляем специфичные заголовки для Star Rail
            if (url.Contains("luna/hkrpg"))
            {
                req.Headers.Add("x-rpc-lrsag", "");
                req.Headers.Add("x-rpc-signgame", "hkrpg");
            }

            res = await _clientData.HttpClient.SendAsync(req);
        }
        else
        {
            HttpContent req = new StringContent("");

            req.Headers.Add("x-rpc-app_version", "1.5.0");
            req.Headers.Add("x-rpc-client_type", "5");
            req.Headers.Add("x-rpc-language", _clientData.Language);
            _clientData.HttpClient.DefaultRequestHeaders.Add("User-Agent", _clientData.UserAgent);
            req.Headers.Add("Cookie", _cookie.GetCookie());

            // Добавляем специфичные заголовки для Star Rail
            if (url.Contains("luna/hkrpg"))
            {
                req.Headers.Add("x-rpc-lrsag", "");
                req.Headers.Add("x-rpc-signgame", "hkrpg");
            }

            res = await _clientData.HttpClient.PostAsync(url, req);
        }

        var jsonString = await res.Content.ReadAsStringAsync();
        var result = JsonNode.Parse(jsonString);

        return result ?? throw new InvalidOperationException("Не удалось разобрать ответ API");
    }
}
