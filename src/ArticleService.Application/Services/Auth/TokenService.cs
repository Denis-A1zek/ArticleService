using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services.Auth;
using ArticleService.Domain.Entities;
using ArticleService.Domain.Interfaces;
using ArticleService.Domain.Settings;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ArticleService.Application.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;
    private readonly JwtOptions _jwtOptions;

    public TokenService(IUnitOfWork unitOfWork, IOptions<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork;
        _jwtOptions = jwtOptions.Value;
        _userRepository = _unitOfWork.GetRepository<User>();
    }
    
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_jwtOptions.JwtKey)); 

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
            credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumbers = new byte[32];
        using var randomNumberGen = RandomNumberGenerator.Create();

        randomNumberGen.GetBytes(randomNumbers);
        return Convert.ToBase64String(randomNumbers);
    }

    public ClaimsPrincipal GetClaimsPrincipal(string accessToken)
    {
        var tokenValidParams = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_jwtOptions.JwtKey)),
            ValidateLifetime = true,
            ValidAudience = _jwtOptions.Audience,
            ValidIssuer = _jwtOptions.Issuer
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = tokenHandler.ValidateToken(accessToken, tokenValidParams, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtToken
            || !jwtToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Неверный токен");

        return claims;
    }

    public async Task<Result<TokenDto>> RefreshToken(TokenDto token)
    {
        string accessToken = token.AccessToken;
        string refreshToken = token.RefreshToken;

        var claims= GetClaimsPrincipal(accessToken);
        var userName = claims.Identity?.Name;

        var user = await _userRepository
            .GetQueryable()
            .Include(u => u.Token)
            .FirstOrDefaultAsync(u => u.Login == userName);

        if (RefreshTokenValidation(user, refreshToken))
            return Result.Failure<TokenDto>("Ошибка при генерации рефреш токена");

        var newAccessToken = GenerateToken(claims.Claims);
        var newRefreshToken = GenerateRefreshToken();

        user.Token.RefreshToken = newRefreshToken;
        _userRepository.Update(user);
        await _unitOfWork.SaveAsync();
        return Result.Success(new TokenDto(newAccessToken, newRefreshToken));
    }

    private bool RefreshTokenValidation(User user, string refreshToken)
        => user is null || 
           user.Token.RefreshToken != refreshToken ||
           user.Token.RefreshTokenExpiryTime < DateTime.UtcNow;
}