using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Api.Endpoints.Auth.Register;

public class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/register", async (
            [FromBody] RegisterUser registerUser,
            [FromServices] IAuthService authService) =>
        {
            var result = await authService.RegisterAsync(registerUser);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result);
        }).WithTags(Tags.Auth).AllowAnonymous();
    }
}