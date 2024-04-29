using ArticleService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Api.Endpoints.Review;

public class GetArticlesForReviewEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("reviews", async (
            [FromServices] IArticleReviewService articleService, 
            CancellationToken cancellationToken) =>
        {
            var result = await articleService.GetForReviewAsync();
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest();
        })
            .WithTags(Tags.Reviews)
            .RequireAuthorization("Review");
    }
}