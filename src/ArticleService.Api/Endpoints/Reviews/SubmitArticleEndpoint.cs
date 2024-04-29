using ArticleService.Api.Models;
using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Api.Endpoints.Review;

public class SubmitArticleEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("reviews/submit", async (
            [FromBody] VerifiedArticleRequest verifiedArticleRequest,
            [FromServices] IArticleReviewService articleService, 
            CancellationToken cancellationToken) =>
        {
            var result = await articleService.SubmitAsync(verifiedArticleRequest.MapToVerifiedArticle());
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result);
        })
            .WithTags(Tags.Reviews)
            .RequireAuthorization("Review");
    }
}