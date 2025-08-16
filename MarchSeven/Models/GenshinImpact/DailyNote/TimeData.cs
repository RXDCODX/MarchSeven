namespace MarchSeven.Models.GenshinImpact.DailyNote;

public class TimeData(TimeSpan ts, DateTime dt)
{
    public TimeSpan TimeSpan { get; set; } = ts;
    public DateTime CompleteTime { get; set; } = dt;
}
