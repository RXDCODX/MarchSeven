namespace MarchSeven.Models.Core;

/// <summary>
/// Базовая конфигурация клиента для HoyoLab API
/// </summary>
public class ClientData
{
    public string UserAgent { get; set; } =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36";
    public EndPoints.EndPoints EndPoints { get; set; } = new();
    public string Language { get; set; } = "en-us";
    public HttpClient HttpClient { get; set; } = new();
}
