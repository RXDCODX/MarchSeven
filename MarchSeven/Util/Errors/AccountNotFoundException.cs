namespace MarchSeven.Util.Errors;

/// <summary>
/// 有効な原神アカウントが見つからなかった際にスローされる例外
/// </summary>
public class AccountNotFoundException : Exception
{
    public AccountNotFoundException()
        : base() { }

    public AccountNotFoundException(string message)
        : base(message) { }

    public AccountNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}
