﻿using MarchSeven.Models.Abstractions;
using MarchSeven.Models.Core;
using MarchSeven.Models.GenshinImpact.DailyNote;
using MarchSeven.Models.ZenlessZoneZero.Entitys;

namespace MarchSeven.Models.ZenlessZoneZero;

/// <summary>
/// Zenless Zone Zero client for HoyoLab WebAPI
/// </summary>
public class ZenlessClient : BaseGameClient
{
    internal ZenlessClient(ICookie cookie)
        : base(cookie) { }

    internal ZenlessClient(ICookie cookie, ClientData data)
        : base(cookie, data) { }

    /// <summary>
    /// Get Zenless Zone Zero statistics (placeholder for future implementation)
    /// </summary>
    public async Task<ZenlessStats.ZenlessStats> FetchZenlessStats(ZenlessUser user)
    {
        var url =
            _clientData.EndPoints.ZenlessStats.Url + $"?server={user.Server}&role_id={user.Uid}";
        return await new Wrapper<ZenlessStats.ZenlessStats>(_clientData).FetchData(
            url,
            _cookie,
            true
        );
    }

    /// <summary>
    /// Get daily note for Zenless Zone Zero (placeholder for future implementation)
    /// </summary>
    public async Task<DailyNote> FetchDailyNote(ZenlessUser user)
    {
        var url =
            _clientData.EndPoints.ZenlessDailyNote.Url
            + $"?server={user.Server}&role_id={user.Uid}";
        return await new Wrapper<DailyNote>(_clientData).FetchData(url, _cookie, true);
    }

    protected override string GetGameBiz()
    {
        return "zzz_global"; // Placeholder for Zenless Zone Zero
    }

    protected override int GetGameId()
    {
        return 8; // Placeholder game ID for Zenless Zone Zero
    }
}
