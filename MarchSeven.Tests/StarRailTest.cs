using Xunit.Abstractions;

namespace MarchSeven.Tests;

public class StarRailTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task TestStarRailRewardData()
    {
        // Create test cookie (you'll need to provide real credentials)
        var cookie = new Cookie { LToken = "your_ltoken_here", LtUid = "your_ltuid_here" };

        // Using root client instead of direct StarRailClient
        using var client = MarchSevenClient.Create(cookie);
        var starRailClient = client.StarRail;

        try
        {
            // Test getting reward data
            var rewardData = await starRailClient.GetStarRailRewardDataAsync();

            Assert.NotNull(rewardData);
            Assert.Equal(0, rewardData.Retcode);
            Assert.NotNull(rewardData.Data);
            Assert.NotNull(rewardData.Data.Awards);

            testOutputHelper.WriteLine($"Month: {rewardData.Data.Month}");
            testOutputHelper.WriteLine($"Total awards: {rewardData.Data.Awards.Length}");

            // Print first few awards
            for (var i = 0; i < Math.Min(5, rewardData.Data.Awards.Length); i++)
            {
                var award = rewardData.Data.Awards[i];
                testOutputHelper.WriteLine($"Award {i + 1}: {award.Name} x{award.Count}");
            }
        }
        catch (Exception ex)
        {
            testOutputHelper.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public Task TestStarRailUserCreation()
    {
        // Test UID for Asia server (starts with 8)
        var user = new StarRailUser(800000000);

        Assert.Equal(800000000, user.Uid);
        Assert.Equal("prod_official_asia", user.Server);

        testOutputHelper.WriteLine($"UID: {user.Uid}");
        testOutputHelper.WriteLine($"Server: {user.Server}");
        return Task.CompletedTask;
    }
}
