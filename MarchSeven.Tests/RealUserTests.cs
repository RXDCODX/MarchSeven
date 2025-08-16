namespace MarchSeven.Tests;

/// <summary>
/// Тесты с использованием реальных куки пользователя
/// </summary>
public class RealUserTests
{
    private readonly CookieV2 _realCookie = TestData.CreateTestCookie();
    private readonly StarRailUser _realStarRailUser = TestData.RealStarRailUid; // Используем implicit converter

    [Fact]
    public void TestRealStarRailUserCreation()
    {
        // Arrange & Act
        var user = new StarRailUser(TestData.RealStarRailUid);

        // Assert
        Assert.Equal(700000000, user.Uid);
        Assert.NotNull(user.Server);
        Assert.Contains("eur", user.Server.ToLower()); // Europe server
    }

    [Fact]
    public void TestRealStarRailUserImplicitConversion()
    {
        // Test implicit conversion from int to StarRailUser
        StarRailUser user = TestData.RealStarRailUid;
        Assert.Equal(700000000, user.Uid);

        // Test implicit conversion from StarRailUser to int
        int uid = user;
        Assert.Equal(700000000, uid);

        // Test implicit conversion from string to StarRailUser
        StarRailUser userFromString = "700000000";
        Assert.Equal(700000000, userFromString.Uid);

        // Test implicit conversion from StarRailUser to string
        string uidString = user;
        Assert.Equal("700000000", uidString);
    }

    [Fact]
    public void TestRealUserServerDetection()
    {
        // Test real user server (Europe - starts with 7)
        var realUser = new StarRailUser(TestData.RealStarRailUid);
        Assert.Contains("eur", realUser.Server.ToLower());

        // Test different server examples
        var asiaUser = new StarRailUser(800000000);
        Assert.Contains("asia", asiaUser.Server.ToLower());

        var americaUser = new StarRailUser(600000000);
        Assert.Contains("usa", americaUser.Server.ToLower());
    }

    [Fact]
    public void TestRealUserInCollections()
    {
        // Test using real UID in collections
        var realUsers = new List<StarRailUser> { TestData.RealStarRailUid, 800000000, 600000000 };
        Assert.Equal(3, realUsers.Count);
        Assert.Equal(700000000, realUsers[0].Uid);
        Assert.Equal(800000000, realUsers[1].Uid);
        Assert.Equal(600000000, realUsers[2].Uid);

        // Test extracting UIDs from collections
        var uids = realUsers.Select(u => (int)u).ToList();
        Assert.Equal([700000000, 800000000, 600000000], uids);
    }

    [Fact]
    public void TestRealUserComparison()
    {
        // Test user comparison using implicit converters
        var user1 = new StarRailUser(TestData.RealStarRailUid);
        var user2 = new StarRailUser(TestData.RealStarRailUid);
        var user3 = new StarRailUser(800000000);

        // Same UID should be equal
        Assert.Equal(user1.Uid, user2.Uid);
        Assert.Equal((int)user1, (int)user2);

        // Different UIDs should not be equal
        Assert.NotEqual(user1.Uid, user3.Uid);
        Assert.NotEqual((int)user1, (int)user3);
    }

    [Fact]
    public void TestRealUserValidation()
    {
        // Test that real UID is valid
        Assert.True(TestData.RealStarRailUid > 0);
        Assert.True(TestData.RealStarRailUid.ToString().Length >= 9);

        // Test that cookie data is valid
        var cookie = TestData.CreateTestCookie();
        Assert.True(cookie.LtMidV2.Length > 0);
        Assert.True(cookie.LtUidV2.Length > 0);

        // Check if we have real cookies or test cookies
        if (TestData.HasEnvironmentSecrets())
        {
            // Real cookies should have long LToken
            Assert.True(cookie.LTokenV2.Length > 100);
        }
        else
        {
            // Test cookies can have shorter values
            Assert.True(cookie.LTokenV2.Length > 0);
        }
    }

    [Fact]
    public void TestRealUserServerLogic()
    {
        // Test that real user server logic works correctly
        var realUser = new StarRailUser(TestData.RealStarRailUid);

        // UID 700000000 should be Europe server
        Assert.Contains("eur", realUser.Server.ToLower());

        // Verify server detection logic - for Star Rail, Europe server is "prod_official_eur"
        var expectedServer = TestData.RealStarRailUid.ToString().StartsWith('7')
            ? "prod_official_eur"
            : "Unknown";
        Assert.Equal(expectedServer, realUser.Server);
    }

    [Fact]
    public void TestRealCookieConsistency()
    {
        // Test that cookie data is consistent across multiple calls
        var cookie1 = TestData.CreateTestCookie();
        var cookie2 = TestData.CreateTestCookie();

        Assert.Equal(cookie1.LtMidV2, cookie2.LtMidV2);
        Assert.Equal(cookie1.LTokenV2, cookie2.LTokenV2);
        Assert.Equal(cookie1.LtUidV2, cookie2.LtUidV2);

        // Test cookie string consistency
        var cookieString1 = cookie1.GetCookie();
        var cookieString2 = cookie2.GetCookie();
        Assert.Equal(cookieString1, cookieString2);
    }
}
