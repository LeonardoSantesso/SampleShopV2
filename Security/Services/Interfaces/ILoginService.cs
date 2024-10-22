using Security.Models;

namespace Security.Services.Interfaces;

public interface ILoginService
{
    Task<Token> AuthenticateAsync(Login userCredentials);
    Task<Token> RefreshTokenAsync(Token token);
    Task<bool> RevokeTokenAsync(string userName);
}