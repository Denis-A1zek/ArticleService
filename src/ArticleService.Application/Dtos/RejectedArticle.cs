namespace ArticleService.Application.Dtos;

public class RejectedArticle
{
    public Guid ArticleId { get; init; }
    public Guid Reviewer { get; init; }
    public string Reason { get; init; }
}