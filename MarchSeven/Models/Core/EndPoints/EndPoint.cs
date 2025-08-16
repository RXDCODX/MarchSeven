namespace MarchSeven.Models.Core.EndPoints;

public class EndPoint(string url, bool isAuth = true, bool isDS = false)
{
    public string Url { get; set; } = url;
    public bool RequireAuth { get; set; } = isAuth;
    public bool RequireDS { get; set; } = isDS;
}
