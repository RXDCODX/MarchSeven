using System.Text.Json.Serialization;
using MarchSeven.Models.Abstractions;

namespace MarchSeven.Models.Core.Cookie;

public class Cookie : ICookie
{
    [JsonPropertyName("ltoken")]
    public string LToken { get; set; } = string.Empty;

    [JsonPropertyName("ltuid")]
    public string LtUid { get; set; } = string.Empty;

    public string GetCookie()
    {
        return $"ltoken={LToken}; ltuid={LtUid}";
    }

    public string GetHoyoUid()
    {
        return LtUid;
    }
}
