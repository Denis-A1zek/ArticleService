using System.Security.Claims;
using CSharpFunctionalExtensions;

namespace ArticleService.Api.Endpoints;

public static class ClaimsUtils
{
    public static Result<Guid> GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        var id =  Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.Sid).Value ?? "");
        if (id == Guid.Empty) return Result.Failure<Guid>("User not identified");
        return id;
    }
}