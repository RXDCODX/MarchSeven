using MarchSeven.Models.Abstractions;

namespace MarchSeven.Models.Core.Cookie;

public class RawCookie(string cookie, string hoyolabUid) : ICookie
{
    public string GetCookie()
    {
        return cookie;
    }

    public string GetHoyoUid()
    {
        return hoyolabUid;
    }
}
