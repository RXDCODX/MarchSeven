namespace MarchSeven.Util.Errors;

/// <summary>
/// HoyoLabAPIから無効な結果が返ってきた際にスローされる例外
/// </summary>
public class HoyoLabApiBadRequestException : Exception
{
    private readonly int _retcode;

    public HoyoLabApiBadRequestException()
        : base() { }

    public HoyoLabApiBadRequestException(string message, int retcode)
        : base(message)
    {
        this._retcode = retcode;
    }

    public HoyoLabApiBadRequestException(string message, Exception innerException)
        : base(message, innerException) { }
}
