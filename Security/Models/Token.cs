namespace Security.Models;

public class Token(bool authenticated, string created, string expiration, string accessToken, string userFullName)
{
    public bool Authenticated { get; set; } = authenticated;
    public string Created { get; set; } = created;
    public string Expiration { get; set; } = expiration;
    public string AccessToken { get; set; } = accessToken;
    public string UserFullName { get; set; } = userFullName;
}
