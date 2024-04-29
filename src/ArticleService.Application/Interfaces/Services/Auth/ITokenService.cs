using System.Security.Claims;
using ArticleService.Application.Dtos;
using CSharpFunctionalExtensions;

namespace ArticleService.Application.Interfaces.Services.Auth;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsPrincipal(string accessToken);
    Task<Result<TokenDto>> RefreshToken(TokenDto token);
}