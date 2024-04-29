using System.Security.Claims;

namespace ArticleService.Api.Endpoints.Auth;

public class UserInfoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("auth/me", async (ClaimsPrincipal user) =>
        {
            
        })
        .WithTags(Tags.Auth);
    }
}