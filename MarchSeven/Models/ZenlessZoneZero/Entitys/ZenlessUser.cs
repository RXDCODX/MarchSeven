using MarchSeven.Models.Abstractions;
using MarchSeven.Models.HoYoLab;

namespace MarchSeven.Models.ZenlessZoneZero.Entitys;

/// <summary>
/// Zenless Zone Zero user representation
/// </summary>
public class ZenlessUser(int uid) : BaseGameUser(uid)
{
    private const int ZenlessGameID = 8; // Placeholder ID

    /// <summary>
    /// Get UID from HoyoLab user stats for Zenless Zone Zero
    /// </summary>
    public static ZenlessUser GetUIDFromHoyoLab(UserStats user)
    {
        return GetUIDFromHoyoLab<ZenlessUser>(user, ZenlessGameID);
    }

    protected override int GameId => ZenlessGameID;

    protected override string GetServer(int uid)
    {
        return Util.Server.Server.GetZenlessServer(uid);
    }

    #region Implicit Converters

    /// <summary>
    /// Implicit conversion from int to ZenlessUser
    /// </summary>
    public static implicit operator ZenlessUser(int uid) => new(uid);

    /// <summary>
    /// Implicit conversion from ZenlessUser to int
    /// </summary>
    public static implicit operator int(ZenlessUser user) => user.Uid;

    /// <summary>
    /// Implicit conversion from string to ZenlessUser
    /// </summary>
    public static implicit operator ZenlessUser(string uid) => new(int.Parse(uid));

    /// <summary>
    /// Implicit conversion from ZenlessUser to string
    /// </summary>
    public static implicit operator string(ZenlessUser user) => user.Uid.ToString();

    #endregion
}
