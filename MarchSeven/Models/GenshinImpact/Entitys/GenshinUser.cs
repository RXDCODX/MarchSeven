using MarchSeven.Models.Abstractions;
using MarchSeven.Models.HoYoLab;

namespace MarchSeven.Models.GenshinImpact.Entitys;

/// <summary>
/// Genshin Impact user representation
/// </summary>
/// <remarks>
/// Genshin Impact user representation
/// </remarks>
public class GenshinUser(int uid) : BaseGameUser(uid)
{
    private const int GenshinGameID = 2;

    /// <summary>
    /// Get UID from HoyoLab user stats for Genshin Impact
    /// </summary>
    public static GenshinUser GetUIDFromHoyoLab(UserStats user)
    {
        return GetUIDFromHoyoLab<GenshinUser>(user, GenshinGameID);
    }

    protected override int GameId => GenshinGameID;

    protected override string GetServer(int uid)
    {
        return Util.Server.Server.GetGenshinServer(uid);
    }

    #region Implicit Converters

    /// <summary>
    /// Implicit conversion from int to GenshinUser
    /// </summary>
    public static implicit operator GenshinUser(int uid) => new(uid);

    /// <summary>
    /// Implicit conversion from GenshinUser to int
    /// </summary>
    public static implicit operator int(GenshinUser user) => user.Uid;

    /// <summary>
    /// Implicit conversion from string to GenshinUser
    /// </summary>
    public static implicit operator GenshinUser(string uid) => new(int.Parse(uid));

    /// <summary>
    /// Implicit conversion from GenshinUser to string
    /// </summary>
    public static implicit operator string(GenshinUser user) => user.Uid.ToString();

    #endregion
}
