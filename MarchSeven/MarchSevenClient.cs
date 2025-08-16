using MarchSeven.Models.Abstractions;
using MarchSeven.Models.Core;
using MarchSeven.Models.GenshinImpact;
using MarchSeven.Models.GenshinImpact.GameRoles;
using MarchSeven.Models.GenshinImpact.Languages;
using MarchSeven.Models.HonkaiStarRail;
using MarchSeven.Models.HoYoLab;
using MarchSeven.Models.HoYoLab.UserAccountInfo;
using MarchSeven.Models.ZenlessZoneZero;

namespace MarchSeven;

/// <summary>
/// Root client that manages all game clients for HoyoLab WebAPI.
/// Use <see cref="Create(ICookie)"/> or <see cref="Create(ICookie, ClientData)"/> to instantiate this class. Do not use the constructor directly.
/// </summary>
public class MarchSevenClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ClientData _clientData;
    private readonly ICookie _cookie;

    // Game clients
    public GenshinClient Genshin { get; }
    public StarRailClient StarRail { get; }
    public ZenlessClient Zenless { get; }

    /// <summary>
    /// Initialize MarchSeven client with cookie
    /// </summary>
    internal MarchSevenClient(ICookie cookie)
    {
        _cookie = cookie;
        _httpClient = new HttpClient();
        _clientData = new ClientData { HttpClient = _httpClient };

        // Initialize game clients
        Genshin = new GenshinClient(cookie, _clientData);
        StarRail = new StarRailClient(cookie, _clientData);
        Zenless = new ZenlessClient(cookie, _clientData);
    }

    /// <summary>
    /// Initialize MarchSeven client with cookie and custom client data
    /// </summary>
    internal MarchSevenClient(ICookie cookie, ClientData clientData)
    {
        _cookie = cookie;
        _httpClient = clientData.HttpClient;
        _clientData = clientData;

        // Initialize game clients
        Genshin = new GenshinClient(cookie, _clientData);
        StarRail = new StarRailClient(cookie, _clientData);
        Zenless = new ZenlessClient(cookie, _clientData);
    }

    /// <summary>
    /// Get user statistics for all games
    /// </summary>
    public async Task<UserStats> FetchUserStats(string? uid = null)
    {
        var targetUid = uid ?? _cookie.GetHoyoUid();
        var url = _clientData.EndPoints.UserStats.Url + $"?uid={targetUid}";
        return await new Wrapper<UserStats>(_clientData).FetchData(url, _cookie);
    }

    /// <summary>
    /// Get user account information by LToken
    /// </summary>
    public async Task<UserAccountInfo> GetUserAccountInfoByLToken()
    {
        var url = _clientData.EndPoints.UserAccountInfo.Url;
        return await new Wrapper<UserAccountInfo>(_clientData).FetchData(url, _cookie);
    }

    /// <summary>
    /// Get game roles for all games
    /// </summary>
    public async Task<GameRoles> GetGameRoles(string region = "")
    {
        var url = _clientData.EndPoints.GetRoles.Url;
        url += "";
        url += !string.IsNullOrEmpty(region) ? $"&region={region}" : "";
        return await new Wrapper<GameRoles>(_clientData).FetchData(url, _cookie);
    }

    /// <summary>
    /// Create MarchSeven client with cookie
    /// </summary>
    public static MarchSevenClient Create(ICookie cookie)
    {
        return new MarchSevenClient(cookie);
    }

    /// <summary>
    /// Create MarchSeven client with cookie and custom client data
    /// </summary>
    public static MarchSevenClient Create(ICookie cookie, ClientData clientData)
    {
        return new MarchSevenClient(cookie, clientData);
    }

    /// <summary>
    /// Get available languages
    /// </summary>
    public static async Task<Languages> GetAvailableLanguages()
    {
        var data = new ClientData();
        return await new Wrapper<Languages>(data).FetchData(data.EndPoints.GetLanguage.Url);
    }

    /// <summary>
    /// Set language for all API requests
    /// </summary>
    public void SetLanguage(string language)
    {
        _clientData.Language = language;
    }

    /// <summary>
    /// Set user agent for all API requests
    /// </summary>
    public void SetUserAgent(string userAgent)
    {
        _clientData.UserAgent = userAgent;
    }

    /// <summary>
    /// Get current client data configuration
    /// </summary>
    public ClientData GetClientData()
    {
        return _clientData;
    }

    /// <summary>
    /// Get underlying HttpClient
    /// </summary>
    public HttpClient GetHttpClient()
    {
        return _httpClient;
    }

    /// <summary>
    /// Dispose the client and underlying HttpClient
    /// </summary>
    public void Dispose()
    {
        _httpClient?.Dispose();
        GC.SuppressFinalize(this);
    }
}
