using Xunit.Abstractions;

namespace MarchSeven.Tests;

public class UserStaminaTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task GetUserStaminaInfo()
    {
        // Create cookies with provided data
        var cookie = TestData.CreateTestCookie();

        // Create client
        using var client = MarchSevenClient.Create(cookie);
        var starRailClient = client.StarRail;

        try
        {
            // Get user roles
            var gameRoles = await client.GetGameRoles();
            var starRailRole = gameRoles.Data?.List?.FirstOrDefault(r =>
                r.GameRegionName == "hkrpg_global"
            );

            if (starRailRole == null)
            {
                testOutputHelper.WriteLine("Star Rail role not found!");
                return;
            }

            var user = new StarRailUser(int.Parse(starRailRole.GameUid));
            testOutputHelper.WriteLine($"UID: {user.Uid}");
            testOutputHelper.WriteLine($"Server: {user.Server}");

            // Get daily note data
            var dailyNote = await starRailClient.FetchDailyNoteAsync(user);

            if (dailyNote?.Data == null)
            {
                testOutputHelper.WriteLine("Failed to get stamina data!");
                return;
            }

            var data = dailyNote.Data;

            // Display stamina information
            testOutputHelper.WriteLine("\n=== STAMINA INFORMATION ===");
            testOutputHelper.WriteLine($"Current Stamina: {data.CurrentStamina}/{data.MaxStamina}");

            // Additional information
            testOutputHelper.WriteLine($"\n=== ADDITIONAL INFORMATION ===");
            testOutputHelper.WriteLine(
                $"Expeditions: {data.AcceptedExpeditionNum}/{data.TotalExpeditionNum}"
            );
            testOutputHelper.WriteLine(
                $"Training Score: {data.CurrentTrainScore}/{data.MaxTrainScore}"
            );
            testOutputHelper.WriteLine(
                $"Rogue Score: {data.CurrentRogueScore}/{data.MaxRogueScore}"
            );

            if (data.Expeditions is { Length: > 0 })
            {
                testOutputHelper.WriteLine($"\n=== EXPEDITIONS ===");
                foreach (var expedition in data.Expeditions)
                {
                    testOutputHelper.WriteLine($"• {expedition.Name}: {expedition.Status}");
                }
            }
        }
        catch (Exception ex)
        {
            testOutputHelper.WriteLine($"Error getting data: {ex.Message}");
            testOutputHelper.WriteLine($"Error type: {ex.GetType().Name}");
        }
    }
}
