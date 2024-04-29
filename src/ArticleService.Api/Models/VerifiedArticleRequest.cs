using ArticleService.Application.Dtos;
using ArticleService.Domain.ValueObjects;

namespace ArticleService.Api.Models;

public class VerifiedArticleRequest
{
    public Guid ArticleId { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init;}

    public VerifiedArticle MapToVerifiedArticle()
    {
        return new VerifiedArticle()
        {
            ArticleId = ArticleId,
            PublicationTime = PublicationTime.Create(Start, End).Value
        };
    }
}