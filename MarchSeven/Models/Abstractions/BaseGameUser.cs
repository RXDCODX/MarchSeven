using MarchSeven.Models.HoYoLab;
using MarchSeven.Util.Errors;

namespace MarchSeven.Models.Abstractions;

/// <summary>
/// Base class for game users
/// </summary>
public abstract class BaseGameUser(int uid)
{
    public int Uid { get; } = uid;
    public string Server => GetServer(Uid);
    protected abstract int GameId { get; }

    /// <summary>
    /// Get UID from HoyoLab user stats
    /// </summary>
    public static T GetUIDFromHoyoLab<T>(UserStats user, int gameId)
        where T : BaseGameUser
    {
        var uid = 0;
        Parallel.ForEach(
            user.Data!.GameLists!,
            game =>
            {
                if (game.GameId == gameId)
                {
                    uid = int.Parse(game.GameUid!);
                }
            }
        );

        return uid == 0
            ? throw new AccountNotFoundException()
            : (T)Activator.CreateInstance(typeof(T), uid)!;
    }

    /// <summary>
    /// Get server based on UID
    /// </summary>
    protected abstract string GetServer(int uid);
}
