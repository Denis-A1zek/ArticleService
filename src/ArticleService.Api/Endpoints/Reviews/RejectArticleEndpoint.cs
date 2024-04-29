using ArticleService.Api.Models;
using ArticleService.Application.Dtos;
using ArticleService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.Api.Endpoints.Review;

public class RejectArticleEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("reviews/reject", async (
            [FromBody] RejectedArticle rejectedArticle,
            [FromServices] IArticleReviewService articleService, 
            CancellationToken cancellationToken) =>
        {
            var result = await articleService.RejectAsync(rejectedArticle);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result);
        })
            .WithTags(Tags.Reviews)
            .RequireAuthorization("Review");
    }
}