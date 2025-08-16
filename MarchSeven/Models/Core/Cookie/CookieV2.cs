using System.Text.Json.Serialization;
using MarchSeven.Models.Abstractions;

namespace MarchSeven.Models.Core.Cookie;

public class CookieV2 : ICookie
{
    [JsonPropertyName("ltoken_v2")]
    public string LTokenV2 { get; set; } = string.Empty;

    [JsonPropertyName("ltmid_v2")]
    public string LtMidV2 { get; set; } = string.Empty;

    [JsonPropertyName("ltuid_v2")]
    public string LtUidV2 { get; set; } = string.Empty;

    public string GetCookie()
    {
        return $"ltoken_v2={LTokenV2}; ltmid_v2={LtMidV2}; ltuid_v2={LtUidV2}";
    }

    public string GetHoyoUid()
    {
        return LtUidV2;
    }
}
