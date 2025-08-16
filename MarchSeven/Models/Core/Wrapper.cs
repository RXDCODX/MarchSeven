﻿using System.Text.Json;
using MarchSeven.Models.Abstractions;
using MarchSeven.Models.GenshinImpact;
using MarchSeven.Util;
using MarchSeven.Util.Errors;

namespace MarchSeven.Models.Core;

internal class Wrapper<T>
    where T : IHoyoLab
{
    private readonly HttpClient _client;
    private readonly string _ua;
    private readonly string _lang;

    internal Wrapper(ClientData data)
    {
        _client = data.HttpClient;
        _ua = data.UserAgent;
        _lang = data.Language;
    }

    internal async Task<T> FetchData(string url, ICookie? cookie = null, bool requireDs = false)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, url);

        req.Headers.Add("x-rpc-app_version", "1.5.0");
        req.Headers.Add("x-rpc-client_type", "5");
        req.Headers.Add("x-rpc-language", _lang);
        req.Headers.Add("user-agent", _ua);
        if (cookie != null)
        {
            req.Headers.Add("Cookie", cookie.GetCookie());
        }

        if (requireDs)
        {
            req.Headers.Add("ds", DynamicSecret.GenerateDynamicSecret());
        }

        var res = await _client.SendAsync(req);
        var jsonString = await res.Content.ReadAsStringAsync();

        if (!res.IsSuccessStatusCode)
        {
            throw new HttpRequestException();
        }
        else if (jsonString.Contains("Login Expired", StringComparison.OrdinalIgnoreCase))
        {
            throw new LoginExpiredException();
        }

        var jsonData =
            JsonSerializer.Deserialize<T>(jsonString) ?? throw new NullReferenceException();

        return jsonData.Retcode != 0
            ? throw new HoyoLabApiBadRequestException(jsonData.Message!, jsonData.Retcode)
            : jsonData;
    }
}
