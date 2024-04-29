using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Api.Endpoints.Auth;

public class RefreshTokenEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/refresh-token", async (
            [FromBody] TokenDto token,
            [FromServices] ITokenService tokenService) =>
        {
            var result = await tokenService.RefreshToken(token);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result);
        }).WithTags(Tags.Auth).AllowAnonymous();
    }
}