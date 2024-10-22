using System.Security.Claims;
using System.Security.Cryptography;
using Common.Helpers;
using Domain.Entities;
using Domain.Repositories;
using Security.Configurations;
using Security.Models;
using Security.Services.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Security.Services;

public class LoginService : ILoginService
{
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
    private readonly TokenConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public LoginService(TokenConfiguration configuration, ITokenService tokenService, IUserRepository userRepository)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<Token> AuthenticateAsync(Login userCredentials)
    {
        var user = await GetUserByLoginAsync(userCredentials);

        if (user == null) 
            return null;

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(ClaimTypes.Name, user.UserName)
        };
       
        var accessToken = _tokenService.GenerateAccessToken(claims);

        DateTime createDate = DateTime.Now;
        DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

        return new Token(
            true,
            createDate.ToString(DateFormat),
            expirationDate.ToString(DateFormat),
            accessToken,
            user.FullName
        );
    }

    public async Task<Token> RefreshTokenAsync(Token token)
    {
        var accessToken = token.AccessToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        
        accessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList());
      
        var createDate = DateTime.Now;
        var expirationDate = createDate.AddMinutes(_configuration.Minutes);

        return new Token(
            true,
            createDate.ToString(DateFormat),
            expirationDate.ToString(DateFormat),
            accessToken,
            token.UserFullName
        );
    }

    public async Task<bool> RevokeTokenAsync(string userName)
    {
       return true;
    }

    private async Task<User?> GetUserByLoginAsync(Login user)
    {
        var password = PasswordHelper.ComputeHash(user.Password, SHA256.Create());
        return await _userRepository.GetUserByLoginAsync(user.UserName, password);
    }

    private async Task<User?> GetUserByUserNameAsync(string userName)
    {
        return await _userRepository.GetUserByUserNameAsync(userName);
    }
}

