namespace MarchSeven.Util.Errors;

/// <summary>
/// ログイン報酬を既に受け取っている際にスローされる例外
/// </summary>
public class DailyRewardAlreadyReceivedException : Exception
{
    public DailyRewardAlreadyReceivedException()
        : base() { }

    public DailyRewardAlreadyReceivedException(string message, Exception innerException)
        : base(message, innerException) { }
}
