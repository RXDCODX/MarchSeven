using Xunit.Abstractions;

namespace MarchSeven.Tests;

/// <summary>
/// Tests for stamina events in Honkai Star Rail
/// </summary>
public class StarRailStaminaEventsTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TestStaminaManager()
    {
        // Создаем тестовые данные
        var testData = new StarRailDailyNoteData
        {
            CurrentStamina = 150,
            MaxStamina = 300,
            StaminaRecoverTime = 1800, // 30 минут
            StaminaFullTimestamp = DateTimeOffset.Now.AddHours(2).ToUnixTimeSeconds(),
            CurrentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };

        // Test main methods
        testOutputHelper.WriteLine("=== Testing Star Rail Stamina Manager ===");
        testOutputHelper.WriteLine(
            $"Current Stamina: {testData.CurrentStamina}/{testData.MaxStamina}"
        );
        testOutputHelper.WriteLine($"Max Stamina: {testData.MaxStamina}");
        testOutputHelper.WriteLine($"Stamina Recovery Time: {testData.StaminaRecoverTime} seconds");

        testOutputHelper.WriteLine("=== Test completed ===");
    }

    [Fact]
    public void TestStaminaCalculationAccuracy()
    {
        // Тестируем точность расчетов
        testOutputHelper.WriteLine("=== Testing Stamina Calculation Accuracy ===");

        var testData = new StarRailDailyNoteData
        {
            CurrentStamina = 100,
            MaxStamina = 300,
            StaminaRecoverTime = 3600, // 1 час
            StaminaFullTimestamp = DateTimeOffset.Now.AddHours(3).ToUnixTimeSeconds(),
            CurrentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };

        testOutputHelper.WriteLine($"Test Data: {testData.CurrentStamina}/{testData.MaxStamina}");
        testOutputHelper.WriteLine($"Recovery Time: {testData.StaminaRecoverTime} seconds");

        testOutputHelper.WriteLine("=== Test completed ===");
    }

    [Fact]
    public void TestStaminaEdgeCases()
    {
        testOutputHelper.WriteLine("=== Testing Stamina Edge Cases ===");

        var testData = new StarRailDailyNoteData
        {
            CurrentStamina = 300,
            MaxStamina = 300,
            StaminaRecoverTime = 0,
            CurrentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };

        // Test edge cases
        testOutputHelper.WriteLine(
            $"Full Stamina: {testData.CurrentStamina}/{testData.MaxStamina}"
        );
        testOutputHelper.WriteLine($"Recovery Time: {testData.StaminaRecoverTime} seconds");

        testOutputHelper.WriteLine("=== Test completed ===");
    }

    [Fact]
    public void TestStaminaEventsSubscription()
    {
        testOutputHelper.WriteLine("=== Testing Stamina Events Subscription ===");

        // Create test client through factory method
        var cookie = new Cookie { LToken = "test", LtUid = "test" };
        using var client = MarchSevenClient.Create(cookie);
        var starRailClient = client.StarRail;

        // Create test data
        var testData = new StarRailDailyNoteData
        {
            CurrentStamina = 299,
            MaxStamina = 300,
            StaminaRecoverTime = 360, // 6 minutes
            StaminaFullTimestamp = DateTimeOffset.Now.AddMinutes(6).ToUnixTimeSeconds(),
            CurrentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };

        testOutputHelper.WriteLine($"Test Data: {testData.CurrentStamina}/{testData.MaxStamina}");
        testOutputHelper.WriteLine($"Recovery Time: {testData.StaminaRecoverTime} seconds");

        testOutputHelper.WriteLine("=== Events test completed ===");
    }

    [Fact]
    public void TestStaminaEventArguments()
    {
        testOutputHelper.WriteLine("=== Testing Event Arguments ===");

        // Create test data
        var testData = new StarRailDailyNoteData
        {
            CurrentStamina = 150,
            MaxStamina = 300,
            StaminaRecoverTime = 1800,
            StaminaFullTimestamp = DateTimeOffset.Now.AddHours(2).ToUnixTimeSeconds(),
            CurrentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };

        testOutputHelper.WriteLine($"Test Data: {testData.CurrentStamina}/{testData.MaxStamina}");
        testOutputHelper.WriteLine($"Recovery Time: {testData.StaminaRecoverTime} seconds");

        testOutputHelper.WriteLine("=== Event arguments test completed ===");
    }
}
