using System.Security.Claims;
using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services;
using ArticleService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using UuidExtensions;

namespace ArticleService.Api.Endpoints;

public class CreateArticleEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("articles", async 
            ([FromBody]EditableArticle article, 
             [FromServices] IArticlesService articleService, 
                ClaimsPrincipal claims,
             CancellationToken cancellationToken) =>
        {
            var resultOfUserParse = ClaimsUtils.GetUserId(claims);
            if (!resultOfUserParse.IsSuccess) return Results.BadRequest(resultOfUserParse);
            var result = await articleService.CreateAsync(resultOfUserParse.Value,article);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest();
        }).WithTags(Tags.Articles);
    }
}