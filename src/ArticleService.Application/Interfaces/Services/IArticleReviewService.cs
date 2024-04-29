using ArticleService.Application.Dtos;
using CSharpFunctionalExtensions;

namespace ArticleService.Application.Interfaces.Services;

public interface IArticleReviewService
{
    Task<Result<IEnumerable<ReviewArticle>>> GetForReviewAsync();
    Task<Result> SubmitAsync(VerifiedArticle verifiedArticle);
    Task<Result> RejectAsync(RejectedArticle rejectedArticle);
}