using ArticleService.Domain.Interfaces;
using ArticleService.Domain.ValueObjects;

namespace ArticleService.Domain.Entities;

public class ArticlesPending : IIdentity
{
    public Guid Id { get; init; }
    public bool Reviewed { get; set; }
    public PublicationTime PublicationTime { get; set; }
    public Guid ArticleId { get; init; }
    public Article? Article { get; set; }
    public Guid? Reviewer { get; set; }
    public string? RejectionMessage { get; set; }
}