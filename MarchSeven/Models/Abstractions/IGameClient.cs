using MarchSeven.Models.GenshinImpact.GameRoles;
using MarchSeven.Models.HoYoLab;
using MarchSeven.Models.HoYoLab.UserAccountInfo;

namespace MarchSeven.Models.Abstractions;

/// <summary>
/// Базовый интерфейс для всех игровых клиентов
/// </summary>
public interface IGameClient
{
    /// <summary>
    /// Получить статистику пользователя
    /// </summary>
    Task<UserStats> FetchUserStatsAsync(string? uid = null);

    /// <summary>
    /// Получить информацию об аккаунте пользователя
    /// </summary>
    Task<UserAccountInfo> GetUserAccountInfoByLTokenAsync();

    /// <summary>
    /// Получить роли пользователя
    /// </summary>
    Task<GameRoles> GetGameRolesAsync(bool isGameOnly = true, string region = "");
}
