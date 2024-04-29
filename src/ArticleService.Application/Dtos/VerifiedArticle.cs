using ArticleService.Domain.ValueObjects;

namespace ArticleService.Application.Dtos;

public record VerifiedArticle
{
    public Guid ArticleId { get; init; }
    public Guid Reviewer { get; init; }
    public PublicationTime PublicationTime { get; init; }
}