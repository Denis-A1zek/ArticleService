namespace ArticleService.Application.Dtos;

public record UserArticle
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Annotation { get; init; }
    public string Description { get; init; } 
    public string? ImageUrl { get; init; }
    public DateTime CreatedAt { get; init; }
}