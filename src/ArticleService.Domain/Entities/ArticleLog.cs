using ArticleService.Domain.Interfaces;
using ArticleService.Domain.ValueObjects;
using UuidExtensions;

namespace ArticleService.Domain.Entities;

public class ArticleLog : IIdentity, IAuditable
{
    public Guid Id { get; init; }
    public Guid ArticleId { get; init; }
    public Article? Article { get; init; }
    public string? Reason { get; init; }
    public Status Status { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public static ArticleLog Create(Guid articleId, Status status, string? reason = null)
    {
        return new ArticleLog()
        {
            Id = Uuid7.Guid(),
            ArticleId = articleId,
            Reason = reason,
            Status = status
        };
    }
}