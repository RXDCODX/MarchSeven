using MarchSeven.Models.Abstractions;
using MarchSeven.Models.HoYoLab;

namespace MarchSeven.Models.HonkaiStarRail.Entitys;

/// <summary>
/// Honkai Star Rail user representation
/// </summary>
public class StarRailUser(int uid) : BaseGameUser(uid)
{
    private const int StarRailGameID = 6;

    /// <summary>
    /// Get UID from HoyoLab user stats for Honkai Star Rail
    /// </summary>
    public static StarRailUser GetUIDFromHoyoLab(UserStats user)
    {
        return GetUIDFromHoyoLab<StarRailUser>(user, StarRailGameID);
    }

    protected override int GameId => StarRailGameID;

    protected override string GetServer(int uid)
    {
        return Util.Server.Server.GetStarRailServer(uid);
    }

    #region Implicit Converters

    /// <summary>
    /// Implicit conversion from int to StarRailUser
    /// </summary>
    public static implicit operator StarRailUser(int uid) => new(uid);

    /// <summary>
    /// Implicit conversion from StarRailUser to int
    /// </summary>
    public static implicit operator int(StarRailUser user) => user.Uid;

    /// <summary>
    /// Implicit conversion from string to StarRailUser
    /// </summary>
    public static implicit operator StarRailUser(string uid) => new(int.Parse(uid));

    /// <summary>
    /// Implicit conversion from StarRailUser to string
    /// </summary>
    public static implicit operator string(StarRailUser user) => user.Uid.ToString();

    #endregion
}
