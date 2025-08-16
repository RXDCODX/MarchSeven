using MarchSeven.Util.Errors;

namespace MarchSeven.Util.Server;

public class Server
{
    /// <summary>
    /// Get server for Genshin Impact based on UID
    /// </summary>
    internal static string GetGenshinServer(int uid)
    {
        return uid.ToString()[..1] switch
        {
            "1" => Servers.CHINA_CELESTIA,
            "2" => Servers.CHINA_CELESTIA,
            "5" => Servers.CHINA_IRUMINSUI,
            "6" => Servers.AMERICA,
            "7" => Servers.EUROPE,
            "8" => Servers.ASIA,
            "9" => Servers.TW_HK_MO,
            _ => throw new AccountNotFoundException(
                "Could not identify the server for this Genshin UID."
            ),
        };
    }

    /// <summary>
    /// Get server for Honkai Star Rail based on UID
    /// </summary>
    internal static string GetStarRailServer(int uid)
    {
        return uid.ToString()[..1] switch
        {
            "1" => Servers.CHINA_CELESTIA,
            "2" => Servers.CHINA_CELESTIA,
            "5" => Servers.CHINA_IRUMINSUI,
            "6" => Servers.STARRAIL_AMERICA,
            "7" => Servers.STARRAIL_EUROPE,
            "8" => Servers.STARRAIL_ASIA,
            "9" => Servers.TW_HK_MO,
            _ => throw new AccountNotFoundException(
                "Could not identify the server for this Star Rail UID."
            ),
        };
    }

    /// <summary>
    /// Get server for Zenless Zone Zero based on UID (placeholder for future)
    /// </summary>
    internal static string GetZenlessServer(int uid)
    {
        // TODO: Implement when Zenless Zone Zero is released
        // This will likely follow the same pattern as other games
        return uid.ToString()[..1] switch
        {
            "1" => Servers.CHINA_CELESTIA,
            "2" => Servers.CHINA_CELESTIA,
            "5" => Servers.CHINA_IRUMINSUI,
            "6" => Servers.AMERICA,
            "7" => Servers.EUROPE,
            "8" => Servers.ASIA,
            "9" => Servers.TW_HK_MO,
            _ => throw new AccountNotFoundException(
                "Could not identify the server for this Zenless UID."
            ),
        };
    }
}
