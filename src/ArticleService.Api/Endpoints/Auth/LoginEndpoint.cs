using System.Security.Claims;
using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Api.Endpoints.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async (
            [FromBody] LoginUser loginRequest,
            [FromServices] IAuthService authService,
            ClaimsPrincipal user) =>
        {
            var result = await authService.LoginAsync(loginRequest);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result);
        }).WithTags(Tags.Auth).AllowAnonymous();
    }
}