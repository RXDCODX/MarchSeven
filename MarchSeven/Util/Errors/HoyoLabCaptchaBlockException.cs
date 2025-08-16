namespace MarchSeven.Util.Errors;

/// <summary>
/// APIがキャプチャ認証でブロックされた際にスローされる例外
/// </summary>
public class HoyoLabCaptchaBlockException : Exception
{
    public HoyoLabCaptchaBlockException()
        : base() { }

    public HoyoLabCaptchaBlockException(string message, Exception innerException)
        : base(message, innerException) { }
}
