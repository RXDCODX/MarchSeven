namespace MarchSeven.Tests;

/// <summary>
/// Тестовые данные для проверки работы API
/// </summary>
public static class TestData
{
    /// <summary>
    /// Создает тестовый CookieV2 с данными пользователя
    /// </summary>
    public static CookieV2 CreateTestCookie()
    {
        // Try to get from environment variables first (for CI/CD)
        var ltMidV2 = Environment.GetEnvironmentVariable("LTMID_V2");
        var lTokenV2 = Environment.GetEnvironmentVariable("LTOKEN_V2");
        var ltUidV2 = Environment.GetEnvironmentVariable("LTUID_V2");

        // Fallback to test values if environment variables are not set
        return
            string.IsNullOrEmpty(ltMidV2)
            || string.IsNullOrEmpty(lTokenV2)
            || string.IsNullOrEmpty(ltUidV2)
            ? new CookieV2
            {
                LtMidV2 = "test_ltmid_v2",
                LTokenV2 = "test_ltoken_v2",
                LtUidV2 = "test_ltuid_v2",
            }
            : new CookieV2
            {
                LtMidV2 = ltMidV2,
                LTokenV2 = lTokenV2,
                LtUidV2 = ltUidV2,
            };
    }

    /// <summary>
    /// Реальный UID пользователя для Honkai Star Rail (Europe server)
    /// </summary>
    public static readonly int RealStarRailUid = 700000000;

    /// <summary>
    /// Тестовый UID для Honkai Star Rail (Asia server) - для демонстрации
    /// </summary>
    public static readonly int TestStarRailUid = 800000000;

    /// <summary>
    /// Тестовый UID для Genshin Impact (Asia server) - для демонстрации
    /// </summary>
    public static readonly int TestGenshinUid = 800000000;

    /// <summary>
    /// Проверяет, доступны ли environment variables для тестов
    /// </summary>
    public static bool HasEnvironmentSecrets()
    {
        return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LTMID_V2"))
            && !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LTOKEN_V2"))
            && !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LTUID_V2"));
    }

    /// <summary>
    /// Проверяет, доступны ли реальные куки пользователя
    /// </summary>
    public static bool HasRealUserCookies()
    {
        var cookie = CreateTestCookie();
        return !string.IsNullOrEmpty(cookie.LtMidV2)
            && !string.IsNullOrEmpty(cookie.LTokenV2)
            && !string.IsNullOrEmpty(cookie.LtUidV2);
    }
}
