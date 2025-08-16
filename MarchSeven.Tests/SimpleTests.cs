using MarchSeven.Models.ZenlessZoneZero.Entitys;

namespace MarchSeven.Tests;

/// <summary>
/// Простые тесты, которые не требуют реальных API вызовов
/// </summary>
public class SimpleTests
{
    [Fact]
    public void TestCookieV2Creation()
    {
        // Arrange & Act
        var cookie = new CookieV2
        {
            LtMidV2 = "test_mid",
            LTokenV2 = "test_token",
            LtUidV2 = "test_uid",
        };

        // Assert
        Assert.Equal("test_mid", cookie.LtMidV2);
        Assert.Equal("test_token", cookie.LTokenV2);
        Assert.Equal("test_uid", cookie.LtUidV2);
    }

    [Fact]
    public void TestGenshinUserCreation()
    {
        // Arrange & Act
        var user = new GenshinUser(123456789);

        // Assert
        Assert.Equal(123456789, user.Uid);
        Assert.NotNull(user.Server);
    }

    [Fact]
    public void TestStarRailUserCreation()
    {
        // Arrange & Act
        var user = new StarRailUser(800000000);

        // Assert
        Assert.Equal(800000000, user.Uid);
        Assert.NotNull(user.Server);
    }

    [Fact]
    public void TestZenlessUserCreation()
    {
        // Arrange & Act
        var user = new ZenlessUser(900000000);

        // Assert
        Assert.Equal(900000000, user.Uid);
        Assert.NotNull(user.Server);
    }

    [Fact]
    public void TestServerDetection()
    {
        // Test Asia server (starts with 8)
        var asiaUser = new StarRailUser(800000000);
        Assert.Contains("asia", asiaUser.Server.ToLower());

        // Test Europe server (starts with 7)
        var europeUser = new StarRailUser(700000000);
        Assert.Contains("eur", europeUser.Server.ToLower());

        // Test America server (starts with 6)
        var americaUser = new StarRailUser(600000000);
        Assert.Contains("usa", americaUser.Server.ToLower());
    }

    [Fact]
    public void TestCookieToString()
    {
        // Arrange
        var cookie = new CookieV2
        {
            LtMidV2 = "test_mid",
            LTokenV2 = "test_token",
            LtUidV2 = "test_uid",
        };

        // Act
        var cookieString = cookie.GetCookie();

        // Assert
        Assert.NotNull(cookieString);
        Assert.Contains("ltmid_v2=test_mid", cookieString);
        Assert.Contains("ltoken_v2=test_token", cookieString);
        Assert.Contains("ltuid_v2=test_uid", cookieString);
    }

    #region Implicit Converter Tests

    [Fact]
    public void TestGenshinUserImplicitConverters()
    {
        // Test int to GenshinUser
        GenshinUser user = 123456789;
        Assert.Equal(123456789, user.Uid);

        // Test GenshinUser to int
        int uid = user;
        Assert.Equal(123456789, uid);

        // Test string to GenshinUser
        GenshinUser userFromString = "123456789";
        Assert.Equal(123456789, userFromString.Uid);

        // Test GenshinUser to string
        string uidString = user;
        Assert.Equal("123456789", uidString);
    }

    [Fact]
    public void TestStarRailUserImplicitConverters()
    {
        // Test int to StarRailUser
        StarRailUser user = 800000000;
        Assert.Equal(800000000, user.Uid);

        // Test StarRailUser to int
        int uid = user;
        Assert.Equal(800000000, uid);

        // Test string to StarRailUser
        StarRailUser userFromString = "800000000";
        Assert.Equal(800000000, userFromString.Uid);

        // Test StarRailUser to string
        string uidString = user;
        Assert.Equal("800000000", uidString);
    }

    [Fact]
    public void TestZenlessUserImplicitConverters()
    {
        // Test int to ZenlessUser
        ZenlessUser user = 900000000;
        Assert.Equal(900000000, user.Uid);

        // Test ZenlessUser to int
        int uid = user;
        Assert.Equal(900000000, uid);

        // Test string to ZenlessUser
        ZenlessUser userFromString = "900000000";
        Assert.Equal(900000000, userFromString.Uid);

        // Test ZenlessUser to string
        string uidString = user;
        Assert.Equal("900000000", uidString);
    }

    [Fact]
    public void TestImplicitConvertersInMethods()
    {
        // Test passing int directly to methods that expect GenshinUser
        var genshinUser = CreateGenshinUser(123456789);
        Assert.Equal(123456789, genshinUser.Uid);

        // Test passing string directly to methods that expect StarRailUser
        var starRailUser = CreateStarRailUser("800000000");
        Assert.Equal(800000000, starRailUser.Uid);

        // Test returning int from methods that return user objects
        var uid = GetGenshinUserUid(new GenshinUser(987654321));
        Assert.Equal(987654321, uid);
    }

    [Fact]
    public void TestImplicitConvertersInCollections()
    {
        // Test using ints in collections that expect users
        var genshinUsers = new List<GenshinUser> { 123456789, 234567890, 345678901 };
        Assert.Equal(3, genshinUsers.Count);
        Assert.Equal(123456789, genshinUsers[0].Uid);
        Assert.Equal(234567890, genshinUsers[1].Uid);
        Assert.Equal(345678901, genshinUsers[2].Uid);

        // Test extracting UIDs from collections
        var uids = genshinUsers.Select(u => (int)u).ToList();
        Assert.Equal([123456789, 234567890, 345678901], uids);
    }

    #endregion

    #region DateTime Conversion Tests

    [Fact]
    public void TestStarRailDailyNoteDataDateTimeConversion()
    {
        // Arrange
        var currentTime = DateTimeOffset.UtcNow;
        var staminaFullTime = currentTime.AddHours(2);

        var dailyNoteData = new StarRailDailyNoteData
        {
            CurrentTimestamp = currentTime.ToUnixTimeSeconds(),
            StaminaFullTimestamp = staminaFullTime.ToUnixTimeSeconds(),
            CurrentStamina = 100,
            MaxStamina = 300,
        };

        // Assert - compare with precision to seconds (Unix timestamp precision)
        Assert.Equal(
            currentTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"),
            dailyNoteData.CurrentTime.ToString("yyyy-MM-dd HH:mm:ss")
        );
        Assert.Equal(
            currentTime.ToString("yyyy-MM-dd HH:mm:ss"),
            dailyNoteData.CurrentTimeOffset.ToString("yyyy-MM-dd HH:mm:ss")
        );
        Assert.Equal(
            staminaFullTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"),
            dailyNoteData.StaminaFullTime.ToString("yyyy-MM-dd HH:mm:ss")
        );
        Assert.Equal(
            staminaFullTime.ToString("yyyy-MM-dd HH:mm:ss"),
            dailyNoteData.StaminaFullTimeOffset.ToString("yyyy-MM-dd HH:mm:ss")
        );
    }

    [Fact]
    public void TestStarRailExpeditionDateTimeConversion()
    {
        // Arrange
        var finishTime = DateTimeOffset.UtcNow.AddHours(1);
        var expedition = new StarRailExpedition
        {
            FinishTimestamp = finishTime.ToUnixTimeSeconds(),
            RemainingTime = 3600, // 1 hour in seconds
            Name = "Test Expedition",
            Status = "Ongoing",
        };

        // Assert - compare with precision to seconds
        Assert.Equal(
            finishTime.DateTime.ToString("yyyy-MM-dd HH:mm:ss"),
            expedition.FinishTime.ToString("yyyy-MM-dd HH:mm:ss")
        );
        Assert.Equal(
            finishTime.ToString("yyyy-MM-dd HH:mm:ss"),
            expedition.FinishTimeOffset.ToString("yyyy-MM-dd HH:mm:ss")
        );
        Assert.Equal(TimeSpan.FromHours(1), expedition.RemainingTimeSpan);
    }

    [Fact]
    public void TestTimestampEdgeCases()
    {
        // Test zero timestamp
        var dailyNoteData = new StarRailDailyNoteData
        {
            CurrentTimestamp = 0,
            StaminaFullTimestamp = 0,
        };

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        Assert.Equal(epoch, dailyNoteData.CurrentTime);
        Assert.Equal(epoch, dailyNoteData.StaminaFullTime);

        // Test negative timestamp (should handle gracefully)
        var negativeTime = new StarRailDailyNoteData
        {
            CurrentTimestamp = -3600, // 1 hour ago
            StaminaFullTimestamp = -7200, // 2 hours ago
        };

        Assert.Equal(epoch.AddHours(-1), negativeTime.CurrentTime);
        Assert.Equal(epoch.AddHours(-2), negativeTime.StaminaFullTime);
    }

    [Fact]
    public void TestExpeditionTimeCalculations()
    {
        // Arrange
        var expedition = new StarRailExpedition
        {
            FinishTimestamp = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds(),
            RemainingTime = 1800, // 30 minutes
            Name = "Time Test Expedition",
        };

        // Assert
        Assert.Equal(TimeSpan.FromMinutes(30), expedition.RemainingTimeSpan);
        Assert.True(expedition.FinishTime > DateTime.UtcNow);
        Assert.True(expedition.FinishTimeOffset > DateTimeOffset.UtcNow);
    }

    #endregion

    #region Helper Methods for Testing

    private static GenshinUser CreateGenshinUser(GenshinUser user) => user;

    private static StarRailUser CreateStarRailUser(StarRailUser user) => user;

    private static int GetGenshinUserUid(GenshinUser user) => user;

    #endregion
}
